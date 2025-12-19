using UnityEngine;

public class EnemyTakeDamage : TakeDamageScript
{
    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        Debug.Log("“G‚ÌHP‚Í" + _userHP);
        _deathManager.EnemyCheckHP(_userHP);
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        Debug.Log("“G‚ÌHP‚Í" + _userHP);
        _deathManager.EnemyCheckHP(_userHP);
    }
}
