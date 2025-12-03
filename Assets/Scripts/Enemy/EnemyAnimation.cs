using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField, Header("アニメーター")]
    private Animator _anim = default;
    public void TakeDamageAnim()
    {
        _anim.SetTrigger("TakeDamage");
    }
}
