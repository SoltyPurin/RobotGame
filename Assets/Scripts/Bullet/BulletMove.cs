using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField, Header("リジッドボディ")]
    private Rigidbody _rigidBody = default;
    [SerializeField, Header("軌跡")]
    private LineRenderer _bulletTrajectory = default;
    private int _bulletDamage = default;
    public int BulletDamage
    {
        get { return _bulletDamage; }
    }

    private int _bulletIndex;
    public int BulletIndex
    {
        get { return _bulletIndex; }
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
    public void StartMove(Vector3 targetDir,int damage,float blowAwayPower,int index,float moveSpeed)
    {
        _rigidBody.linearVelocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _bulletDamage = damage;
        _blowAwayPower = blowAwayPower;
        _bulletDirection = targetDir;
        _bulletTrajectory.transform.localRotation = Quaternion.LookRotation(-targetDir, Vector3.up);
        _rigidBody.AddForce(_bulletDirection * moveSpeed,ForceMode.Impulse);
        _bulletIndex = index;
    }

}
