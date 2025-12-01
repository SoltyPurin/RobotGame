using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("ダッシュ力")]
    private float _dashPower = 10f;

    public void DashProtocol(Vector3 direction)
    {
        Debug.Log("ダッシュ");
        direction.y = 0;
        _ballRigidBody.AddForce(direction * _dashPower, ForceMode.Impulse);
    }
}
