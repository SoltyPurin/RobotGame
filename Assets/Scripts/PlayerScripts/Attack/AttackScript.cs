using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のスクリプト")]
    private LeftAttack _meleeScript = default;

    public void RightAttack()
    {
        Debug.Log("右手で攻撃");
    }

    public void LeftAttack(Transform target)
    {
        Debug.Log("左手で攻撃");
        _meleeScript.SetTargetAndRushStart(target);
    }
}
