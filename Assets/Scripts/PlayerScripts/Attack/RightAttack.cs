using UnityEngine;

public class RightAttack : MonoBehaviour
{
    [SerializeField, Header("e’e‚Ì¶‘¶ŠÔ")]
    private float _bulletAliveTime = 5;
    [SerializeField, Header("’e‚ª—^‚¦‚éƒ_ƒ[ƒW")]
    private int _bulletDamage = 50;
    [SerializeField, Header("e’e‚Ì‘¬“x")]
    private float _bulletSpeed = 140;
    [SerializeField, Header("‚«”ò‚Î‚µ—Í")]
    private float _blowAwayPower = 50f;
    private PlayerSoundPlayScript _soundPlay = default;
    private BulletPool _pool = default;

    private void Awake()
    {
        _soundPlay = FindAnyObjectByType<PlayerSoundPlayScript>();
        _pool = GameObject.FindWithTag("BulletPool").GetComponent<BulletPool>();
        _bulletSpeed += PlayerPrefs.GetInt(AssemblyPointDispatcher.BulletSpeed);
    }
    public void ShootProtocol(Transform target)
    {
        Vector3 targetDIr = (target.position - transform.position).normalized;
        for(int i =0; i<3; i++)
        {
            _pool.ActiveBullet(targetDIr, _bulletAliveTime, transform.position, _bulletDamage, _blowAwayPower, "PLBullet",_bulletSpeed);
            _soundPlay.PlayShootSound();
        }
    }
}
