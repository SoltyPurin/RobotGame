using UnityEngine;

public class EnemyTakeDamage : TakeDamageScript
{
    private EnemyHPSlider _slider = default;
    public override void Start()
    {
        base.Start();
        _slider = GetComponent<EnemyHPSlider>();
        _slider.Initialize(_userHP);
    }
    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        _deathManager.EnemyCheckHP(_userHP);
        _slider.ValueUpdate(_userHP);
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        _deathManager.EnemyCheckHP(_userHP);
        _slider.ValueUpdate(_userHP);

    }
}
