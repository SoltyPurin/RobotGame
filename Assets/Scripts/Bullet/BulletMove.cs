using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField, Header("リジッドボディ")]
    private Rigidbody _rigidBody = default;
    [SerializeField, Header("銃弾の速度")]
    private float _moveSpeed = 50f;
    public void StartMove(Vector3 targetDir)
    {
        _rigidBody.linearVelocity = targetDir * _moveSpeed;
    }
}
