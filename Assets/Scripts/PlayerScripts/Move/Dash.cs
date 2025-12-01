using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("ダッシュ力")]
    private float _dashPower = 10f;

    public void DashProtocol(Vector3 velocity)
    {
        Debug.Log("ダッシュ");
        velocity.y = 0;
        _ballRigidBody.AddForce(velocity * _dashPower, ForceMode.Impulse);
    }
}
