using UnityEngine;

public class RightAttack : MonoBehaviour
{
    [SerializeField, Header("銃弾の生存時間")]
    private float _bulletAliveTime = 5;
    [SerializeField, Header("弾が与えるダメージ")]
    private int _bulletDamage = 50;
    [SerializeField, Header("吹き飛ばし力")]
    private float _blowAwayPower = 50f;
    [SerializeField, Header("音を再生するスクリプト")]
    private PlayerSoundPlayScript _soundPlay = default;
    private BulletPool _pool = default;

    private void Awake()
    {
        _pool = GameObject.FindWithTag("BulletPool").GetComponent<BulletPool>();
    }
    public void ShootProtocol(Transform target)
    {
        Vector3 targetDIr = (target.position - transform.position).normalized;
        for(int i =0; i<3; i++)
        {
            _pool.ActiveBullet(targetDIr, _bulletAliveTime, transform.position, _bulletDamage, _blowAwayPower, "PLBullet");
            _soundPlay.PlayShootSound();
        }
    }
}
