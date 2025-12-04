using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField, Header("リジッドボディ")]
    private Rigidbody _rigidBody = default;
    [SerializeField, Header("銃弾の速度")]
    private float _moveSpeed = 50f;
    private float _currentAliveTime = 0;
    private bool _canMove = false;
    private Vector3 _targetDirection = Vector3.zero;
    public void StartMove(Vector3 targetDir)
    {
        _targetDirection = targetDir;
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (!_canMove)
        {
            return;
        }
        _rigidBody.linearVelocity = _targetDirection * _moveSpeed * Time.fixedDeltaTime;
    }
}
