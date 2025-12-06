using UnityEngine;

public class RightAttack : MonoBehaviour
{
    [SerializeField, Header("e’e‚Ì¶‘¶ŠÔ")]
    private float _bulletAliveTime = 5;
    [SerializeField, Header("’e‚ª—^‚¦‚éƒ_ƒ[ƒW")]
    private int _bulletDamage = 50;
    [SerializeField, Header("‚«”ò‚Î‚µ—Í")]
    private float _blowAwayPower = 50f;
    private BulletPool _pool = default;

    private void Awake()
    {
        _pool = GameObject.FindWithTag("BulletPool").GetComponent<BulletPool>();
    }
    public void ShootProtocol(Transform target)
    {
        Vector3 targetDIr = (target.position - transform.position).normalized;
        _pool.ActiveBullet(targetDIr, _bulletAliveTime,this.transform.position,_bulletDamage,_blowAwayPower);
    }
}
