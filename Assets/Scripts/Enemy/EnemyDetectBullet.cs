using UnityEngine;

public class EnemyDetectBullet : MonoBehaviour
{
    [SerializeField, Header("îÌíeÉXÉNÉäÉvÉg")]
    private TakeDamageScript _takeDamage = default;

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
        Debug.Log("èeíeãÚÇÁÇ¡ÇΩ");
        BulletMove bulletMove = bullet.GetComponent<BulletMove>();
        Vector3 bulletDirection = bulletMove.BulletDirection;
        int damage = bulletMove.BulletDamage;
        float blowAway = bulletMove.BlowAwayPower;
        _takeDamage.ShootTakeDamage(bulletDirection, damage, blowAway);
    }
}
