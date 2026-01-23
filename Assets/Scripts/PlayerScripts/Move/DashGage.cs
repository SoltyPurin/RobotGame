using UnityEngine;
using UnityEngine.InputSystem;

public class DashGage : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("ダッシュ力")]
    private float _dashPower = 10f;

    public void DashProtocol(Vector3 direction)
    {
        direction.y = 0;
        _ballRigidBody.AddForce(direction * _dashPower, ForceMode.Impulse);
    }
}
