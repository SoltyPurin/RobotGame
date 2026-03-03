using System.Collections;
using UnityEngine;

public class EnemyTakeDamage : TakeDamageScript
{
    [SerializeField, Header("Ž€–SŽž‚É’x‚ç‚¹‚éŽžŠÔ")]
    private float _deathDelayTime = 2;
    private AuraBurstPerformance _burstPerformance = default;
    private TestAIController _controller = default;
    private EnemySoundPlayScript _soundPlay = default;
    private EnemyHPSlider _slider = default;

    private int _damgerHP = 0;

    private bool _canUseBurst = true;
    public override void Start()
    {
        base.Start();
        _burstPerformance = FindAnyObjectByType<AuraBurstPerformance>();
        _soundPlay = GetComponent<EnemySoundPlayScript>();
        _controller = GetComponent<TestAIController>();
    }


    public void SetStatus(EnemyStatus status)
    {
        _damgerHP = status.EscapeHPThreshould;
        _userHP = status.EnemyHP;
        _slider = GetComponent<EnemyHPSlider>();
        _slider.Initialize(_userHP);
    }
    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        _deathManager.EnemyCheckHP(_userHP,this.gameObject);
        _slider.ValueUpdate(_userHP);
        _soundPlay.PlayTakeMeleeDamage();
        DeathCheck();
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        _deathManager.EnemyCheckHP(_userHP,this.gameObject);
        _slider.ValueUpdate(_userHP);
        _soundPlay.PlayeTakeShotDamage();
        DeathCheck();
    }

    public override void DeathCheck()
    {
        base.DeathCheck();
        if(_userHP <= _damgerHP && _canUseBurst)
        {
            _canUseBurst = false;
            _burstPerformance.AuraBurstCutIn(this.gameObject);
        }
        if (_userHP <= 0)
        {
            _soundPlay.PlayDeathExplosionSE();
            _controller.Dead();
            StartCoroutine(DeathDelay());
        }
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(_deathDelayTime);
        LockOn lockOn = GameObject.FindAnyObjectByType<LockOn>();
        lockOn.UnlockTarget();
        lockOn.ChangeCamera();
        Destroy(this.gameObject);
    }
}
