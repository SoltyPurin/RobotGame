using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField, Header("吹き飛びのスクリプト")]
    private EnemyBlowAway _blowAway = default;
    public void TakeDamage(Vector3 playerDirection,float damage,float blowAwayPower)
    {
        Debug.Log("攻撃喰らった");
         _blowAway.BlowAwayProtocol(playerDirection, blowAwayPower);
    }
}
