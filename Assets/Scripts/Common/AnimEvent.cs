using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    [SerializeField, Header("被弾スクリプト")]
    private TakeDamageScript _takeDamage = default;

    public void EndDamageProtocol()
    {
        _takeDamage.EndBlowAway();
    }
}
