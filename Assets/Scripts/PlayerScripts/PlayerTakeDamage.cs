using UniRx;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerTakeDamage : TakeDamageScript
{
    [SerializeField, Header("ãﬂê⁄çUåÇêHÇÁÇ¡ÇΩéûÇÃóhÇÍ")]
    private float _meleeDuration = 1;
    [SerializeField, Header("éÀåÇçUåÇêHÇÁÇ¡ÇΩéûÇÃóhÇÍ")]
    private float _shootDuration = 0.5f;
    [SerializeField, Header("ê≥ñ Ç∆îªífÇ∑ÇÈäpìx")]
    private float _frontAngle = 20;
    [SerializeField, Header("ÉçÉbÉNÉIÉìÉJÉÅÉâ")]
    private CinemachineCamera _lockOnCamera = default;
    
    private PlayerSoundPlayScript _soundPlayer = default;
    private DamageEffect _effect = default;
    private PlayerVibration _vibration = default;
    private LockOn _lockOn = default;
    private UIViewer _uiViewer = default;
    private PlayerInputManager _input = default;
    private CinemachineImpulseSource _shake = default;
    private AuraBurst _auraBurst = default;

    private int _halfHP = 0;
    private bool _isInvincible = false; 

    public override void Start()
    {
        base.Start();
        _soundPlayer = FindAnyObjectByType<PlayerSoundPlayScript>();
        _effect = FindAnyObjectByType<DamageEffect>();
        _vibration = GetComponent<PlayerVibration>();
        _lockOn = GetComponent<LockOn>();   
        _shake = GetComponent<CinemachineImpulseSource>();
        _uiViewer = FindAnyObjectByType<UIViewer>();
        _input = GetComponent<PlayerInputManager>();
        _uiViewer.SetHealth(_userHP);
        _auraBurst = GetComponent<AuraBurst>();
        _halfHP = _userHP / 2;
    }

    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        if (_isInvincible)
        {
            return; 
        }
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_meleeDuration);
        }
        _uiViewer.SetHealth(_userHP);
        CallViberationCoroutine(attackDirection, _meleeDuration,true);
        _soundPlayer.PlayMeleeTakeDamage();
        DeathCheck();
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        if (_isInvincible)
        {
            return;
        }

        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_shootDuration);

        }
        _uiViewer.SetHealth(_userHP);
        CallViberationCoroutine(bulletDirection, _shootDuration,false);
        _soundPlayer.PlayShotTakeDamage();
        DeathCheck();
    }

    public override void DeathCheck()
    {
        base.DeathCheck();
        if(_userHP <= 0)
        {
            _lockOnCamera.LookAt = transform;
            _soundPlayer.PlayDeathSound();
            _input.enabled = false;
            _deathManager.PlayerCheckHP(_userHP);
        }
        if(_userHP <= _halfHP)
        {
            _auraBurst.AuraBurstUseableProtocol();
        }
    }

    private void CallViberationCoroutine(Vector3 attackDirection,float duration,bool isMelee)
    {
        float left = 0;
        float right = 0;
        switch (CalcEnemyAttackDirection(attackDirection))
        {
            case 0:
                right = 1 * duration;
                break;

            case 1:
                left = 1 * duration;
                break;

            default:
                left = 1 * duration;
                right = 1 * duration;
                break;
        }
        if (isMelee)
        {
            StartCoroutine(_vibration.MeleeDamageVibe(left, right));
        }
        else
        {
            StartCoroutine(_vibration.ShootDamageVibe(left, right));
        }
    }
    /// <summary>
    /// çUåÇÇ™ê≥ñ ç∂âEÇ«ÇøÇÁÇ©Ç©ÇîªífÇ∑ÇÈ
    /// </summary>
    /// <param name="toPlayer"></param>
    /// <returns>0ÇÕâEÅA1ÇÕç∂ÅA2ÇÕê≥ñ </returns>
    private int CalcEnemyAttackDirection(Vector3 toPlayer)
    {
        if(_lockOn.TargetTransform == null)
        {
            return 2;
        }
        Vector3 baseDir = (_lockOn.TargetTransform.position-_lockOnCamera.transform.position).normalized;
        float angle = Vector3.SignedAngle(baseDir, toPlayer,Vector3.up);
        bool isFrontAttack = Mathf.Abs(angle) <= _frontAngle;
        if (isFrontAttack)
        {
            _effect.FrontDamage();
            return 2;
        }

        if(angle > 0)
        {
            _effect.RightDamage();
            return 0;
        }
        else
        {
            _effect.LeftDamage();
            return 1;
        }
    }

    public void AuraBurst(bool isAuraBursting)
    {
        _isInvincible = isAuraBursting;
    }
}
