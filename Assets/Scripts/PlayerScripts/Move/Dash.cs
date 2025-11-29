using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("ダッシュ力")]
    private float _dashPower = 10f;

    private InputAction _dashButton = default;
    private PlayerMove _move = default;
    private void Start()
    {
        _dashButton = InputSystem.actions.FindAction("Dash");
        _move = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if (_dashButton.WasPressedThisFrame())
        {
            DashProtocol();
        }
    }

    private void DashProtocol()
    {
        Debug.Log("ダッシュ");
        _ballRigidBody.AddForce(_move.UseVelocity * _dashPower, ForceMode.Impulse);
    }
}
