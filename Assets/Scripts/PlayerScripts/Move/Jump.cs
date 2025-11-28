using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")]
    private float _jumpForce = 10f;
    [SerializeField, Header("何回ジャンプできるか")]
    private int _canJumpCount = 2;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;

    private InputAction _jumpButton;

    private int _jumpCount = 0;

    private void Start()
    {
        _jumpButton = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if(_jumpCount >= _canJumpCount)
        {
            return;
        }
        if (_jumpButton.WasPressedThisFrame())
        {
            JumpProtocol();
        }
    }

    private void JumpProtocol()
    {
        Debug.Log("ジャンプ");
        _jumpCount++;
        _ballRigidBody.AddForce(transform.up *  _jumpForce,ForceMode.Impulse);
    }

    public void JumpCountReset()
    {
        _jumpCount = 0;
    }
}
