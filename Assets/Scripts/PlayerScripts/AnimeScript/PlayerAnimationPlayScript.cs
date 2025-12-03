using UnityEngine;

public class PlayerAnimationPlayScript : MonoBehaviour
{
    [SerializeField, Header("アニメーター")]
    private Animator _animator = default;
    public void IdleAnim()
    {

        _animator.SetBool("Moveing",false);
    }

    public void MoveAnim()
    {
        _animator.SetBool("Moveing", true);
    }

    public void JumpAnim()
    {
        _animator.SetTrigger("Jump");
    }

    public void RightAttackAnim()
    {
        _animator.SetTrigger("Shoot");
    }

    public void LeftATKRush()
    {
        _animator.SetTrigger("MeleeRush");
    }

    public void LeftATKProtocol()
    {
        _animator.SetTrigger("MeleeAttack");
    }

    public void TakeDamageAnim()
    {
        _animator.SetTrigger("TakeDamage");
    }
}
