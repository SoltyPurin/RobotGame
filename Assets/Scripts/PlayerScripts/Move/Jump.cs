using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")]
    protected float _jumpForce = 10f;
    [SerializeField, Header("何回ジャンプできるか")]
    protected int _canJumpCount = 2;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;

    protected int _jumpCount = 0;

    public virtual void JumpProtocol()
    {
        if (_jumpCount >= _canJumpCount)
        {
            return;
        }

        _jumpCount++;
        _ballRigidBody.AddForce(transform.up *  _jumpForce,ForceMode.Impulse);
    }

    public void JumpCountReset()
    {
        _jumpCount = 0;
    }
}
