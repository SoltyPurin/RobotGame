using UnityEngine;

public class EnemyDetectBullet : MonoBehaviour
{
    [SerializeField, Header("被弾スクリプト")]
    private EnemyTakeDamage _takeDamage = default;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.CompareTag("Bullet"))
        {
            CallTakeDamageMethod(obj);
        }
    }

    private void CallTakeDamageMethod(GameObject bullet)
    {
        BulletMove bulletMove = bullet.GetComponent<BulletMove>();
        Vector3 bulletDirection = bulletMove.BulletDirection;
        int damage = bulletMove.BulletDamage;
        float blowAway = bulletMove.BlowAwayPower;
        _takeDamage.ShootTakeDamage(bulletDirection, damage, blowAway);
    }
}
