using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField, Header("リジッドボディ")]
    private Rigidbody _rigidBody = default;
    [SerializeField, Header("銃弾の最初の力")]
    private float _moveSpeed = 50f;
    private int _bulletDamage = default;
    public int BulletDamage
    {
        get { return _bulletDamage; }
    }

    private Vector3 _bulletDirection = Vector3.zero;
    public Vector3 BulletDirection
    {
        get { return _bulletDirection; }
    }
    private float _blowAwayPower = 0;
    public float BlowAwayPower
    {
        get { return _blowAwayPower; }
    }
    public void StartMove(Vector3 targetDir,int damage,float blowAwayPower)
    {
        _bulletDamage = damage;
        _blowAwayPower = blowAwayPower;
        _rigidBody.AddForce(targetDir * _moveSpeed,ForceMode.Impulse);
    }

}
