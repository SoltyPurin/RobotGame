using UnityEngine;

public class RightAttack : MonoBehaviour
{
    [SerializeField, Header("èeíeÇÃê∂ë∂éûä‘")]
    private float _bulletAliveTime = 5;

    private BulletPool _pool = default;

    private void Awake()
    {
        _pool = GameObject.FindWithTag("BulletPool").GetComponent<BulletPool>();
    }
    public void ShootProtocol(Transform target)
    {
        Vector3 targetDIr = (target.position - transform.position).normalized;
        _pool.ActiveBullet(targetDIr, _bulletAliveTime);
    }
}
