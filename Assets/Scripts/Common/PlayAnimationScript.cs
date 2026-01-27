using UnityEngine;

public class PlayAnimationScript : MonoBehaviour
{
    [SerializeField, Header("アニメーター")]
    private Animator _animator = default;
    public void IdleAnim()
    {
        if(_animator == null)
        {
            return;
        }
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

    public void DeathAnim()
    {
        _animator.SetBool("IsDead", true);
    }
}
