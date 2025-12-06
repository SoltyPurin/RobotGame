using UnityEngine;

public class EnemyGravity : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("重力")]
    private float _downForce = 150;

    private void FixedUpdate()
    {
        _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);
    }
}
