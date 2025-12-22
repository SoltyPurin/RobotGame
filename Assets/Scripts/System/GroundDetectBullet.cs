using UnityEngine;

public class GroundDetectBullet : MonoBehaviour
{
    private BulletPool _bulletPool = default;

    private void Start()
    {
        _bulletPool = GameObject.FindAnyObjectByType<BulletPool>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObj = collision.gameObject;
        if(colObj.CompareTag("ENBullet") || colObj.CompareTag("PLBullet"))
        {
            Debug.Log("èeíeÇåüím");
            _bulletPool.DeActiveBullet(colObj);
        }
    }
}
