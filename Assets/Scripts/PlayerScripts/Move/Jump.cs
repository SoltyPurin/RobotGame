using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")]
    private float _jumpForce = 10f;

    private Rigidbody _onBallRigidBody = default;

    private InputAction _jumpButton;

    private void Start()
    {
        _onBallRigidBody = GetComponent<Rigidbody>();
        _jumpButton = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if (_jumpButton.WasPressedThisFrame())
        {
            JumpProtocol();
        }
    }

    private void JumpProtocol()
    {
        Debug.Log("ジャンプ");
        _onBallRigidBody.AddForce(transform.up *  _jumpForce);
    }
}
