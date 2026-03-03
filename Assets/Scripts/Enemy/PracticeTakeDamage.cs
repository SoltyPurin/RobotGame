using UnityEngine;

public class PracticeTakeDamage : TakeDamageScript
{
    [SerializeField, Header("ó˚èKÇÃìGÇÃëÃóÕ")]
    private int _practiceHelth = 1000;

    private EnemySoundPlayScript _soundPlay = default;
    private EnemyHPSlider _slider = default;
    private PracticeEnemyDeathAleart _deathAleart = default;

    public override void Start()
    {
        base.Start();
        _userHP = _practiceHelth;
        _soundPlay = GetComponent<EnemySoundPlayScript>();
        _slider = GetComponent<EnemyHPSlider>();
        _slider.Initialize(_userHP);
        _deathAleart = GetComponent<PracticeEnemyDeathAleart>();
    }

    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        _slider.ValueUpdate(_userHP);
        _soundPlay.PlayTakeMeleeDamage();
        DeathCheck();
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        _slider.ValueUpdate(_userHP);
        _soundPlay.PlayeTakeShotDamage();
        DeathCheck();
    }

    public override void DeathCheck()
    {
        base.DeathCheck();
        if (_userHP <= 0)
        {
            _soundPlay.PlayDeathExplosionSE();
            _deathAleart.DeathProtocol();
            Destroy(this.gameObject);
        }
    }


}
