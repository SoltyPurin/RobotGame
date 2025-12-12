using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のスクリプト")]
    private LeftAttack _meleeScript = default;
    [SerializeField, Header("射撃のスクリプト")]
    private RightAttack _shootScript = default;

    public void RightAttack(Transform target)
    {
        _shootScript.ShootProtocol(target);
    }

    public void LeftAttack(Transform target)
    {
        _meleeScript.SetTargetAndRushStart(target);
    }
}
