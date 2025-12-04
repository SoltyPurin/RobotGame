using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のスクリプト")]
    private LeftAttack _meleeScript = default;
    [SerializeField, Header("射撃のスクリプト")]
    private RightAttack _shootScript = default;

    public void RightAttack(Transform target)
    {
        Debug.Log("右手で攻撃");
        _shootScript.ShootProtocol(target);
    }

    public void LeftAttack(Transform target)
    {
        Debug.Log("左手で攻撃");
        _meleeScript.SetTargetAndRushStart(target);
    }
}
