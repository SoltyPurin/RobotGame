using System.Collections;
using UnityEngine;

public class EnemyTakeDamage : TakeDamageScript
{
    [SerializeField, Header("Ž€–SŽž‚É’x‚ç‚¹‚éŽžŠÔ")]
    private float _deathDelayTime = 2;

    private TestAIController _controller = default;
    private PlayAnimationScript _anim = default;
    private EnemySoundPlayScript _soundPlay = default;
    private EnemyHPSlider _slider = default;
    public override void Start()
    {
        base.Start();
        _anim = GetComponent<PlayAnimationScript>();
        _soundPlay = GetComponent<EnemySoundPlayScript>();
        _controller = GetComponent<TestAIController>();
        _slider = GetComponent<EnemyHPSlider>();
        _slider.Initialize(_userHP);
    }
    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        _deathManager.EnemyCheckHP(_userHP);
        _slider.ValueUpdate(_userHP);
        _soundPlay.PlayTakeMeleeDamage();
        CheckDeath();
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        _deathManager.EnemyCheckHP(_userHP);
        _slider.ValueUpdate(_userHP);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if(_userHP <= 0)
        {
            _controller.Dead();
            StartCoroutine(DeathDelay());
            _anim.DeathAnim();
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
