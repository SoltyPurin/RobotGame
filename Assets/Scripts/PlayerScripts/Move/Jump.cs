using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")]
    protected float _jumpForce = 10f;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("エフェクト")]
    private PlayerEffectPlay _effect = default;

    private bool _canJump = true;

    public virtual void JumpProtocol()
    {
        if (!_canJump)
        {
            return;
        }
        _effect.PlayThrusterEffect();
        _ballRigidBody.AddForce(transform.up *  _jumpForce,ForceMode.Impulse);
    }

    public void CanJumpSwitch()
    {
        _canJump = true;
    }

    public void CantJumpSwitch()
    {
        _canJump = false;
    }
}
