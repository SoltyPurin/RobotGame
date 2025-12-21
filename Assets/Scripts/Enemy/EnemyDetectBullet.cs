using UnityEngine;

public class EnemyDetectBullet : MonoBehaviour
{
    private TakeDamageScript _takeDamage = default;
    private BulletPool _pool = default;

    private void Start()
    {
        _pool = GameObject.FindAnyObjectByType<BulletPool>();
        _takeDamage = GetComponent<TakeDamageScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.CompareTag("PLBullet"))
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
        _pool.DeActiveBullet(bullet);
    }
}
