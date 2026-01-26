using Unity.Cinemachine;
using UnityEngine;

public class PlayerTakeDamage : TakeDamageScript
{
    [SerializeField, Header("‹ßÚUŒ‚H‚ç‚Á‚½‚Ì—h‚ê")]
    private float _meleeDuration = 1;
    [SerializeField, Header("ËŒ‚UŒ‚H‚ç‚Á‚½‚Ì—h‚ê")]
    private float _shootDuration = 0.5f;
    [SerializeField, Header("³–Ê‚Æ”»’f‚·‚éŠp“x")]
    private float _frontAngle = 20;
    [SerializeField, Header("ƒƒbƒNƒIƒ“ƒJƒƒ‰")]
    private CinemachineCamera _lockOnCamera = default;

    private PlayerSoundPlayScript _soundPlayer = default;
    private DamageEffect _effect = default;
    private PlayerVibration _vibration = default;
    private LockOn _lockOn = default;
    private UIViewer _uiViewer = default;
    private PlayerInputManager _input = default;
    private CinemachineImpulseSource _shake = default;

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
    }

    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_meleeDuration);
        }
        _uiViewer.SetHealth(_userHP);
        CallViberationCoroutine(attackDirection, _meleeDuration);
        _soundPlayer.PlayMeleeTakeDamage();
        DeathCheck();
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_shootDuration);

        }
        _uiViewer.SetHealth(_userHP);
        CallViberationCoroutine(bulletDirection, _shootDuration);
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
            _input.Dead();
            _deathManager.PlayerCheckHP(_userHP);
        }
    }

    private void CallViberationCoroutine(Vector3 attackDirection,float duration)
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
        StartCoroutine(_vibration.MeleeDamageVibe(left, right));
    }
    /// <summary>
    /// UŒ‚‚ª³–Ê¶‰E‚Ç‚¿‚ç‚©‚©‚ğ”»’f‚·‚é
    /// </summary>
    /// <param name="toPlayer"></param>
    /// <returns>0‚Í‰EA1‚Í¶A2‚Í³–Ê</returns>
    private int CalcEnemyAttackDirection(Vector3 toPlayer)
    {
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
}
