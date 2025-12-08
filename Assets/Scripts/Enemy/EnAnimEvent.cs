using UnityEngine;

public class EnAnimEvent : MonoBehaviour
{
    [SerializeField, Header("被弾スクリプト")]
    private EnemyTakeDamage _takeDamage = default;

    public void EndDamageProtocol()
    {
        _takeDamage.EndBlowAway();
    }
}
