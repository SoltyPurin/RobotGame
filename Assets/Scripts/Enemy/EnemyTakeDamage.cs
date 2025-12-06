using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField, Header("吹き飛びのスクリプト")]
    private BlowAway _blowAway = default;
    [SerializeField, Header("アニメーション再生のスクリプト")]
    private PlayAnimationScript _anim = default;
    public void MeleeTakeDamage(Vector3 playerDirection,float damage,float blowAwayPower)
    {
        Debug.Log("近接攻撃喰らった");
         _blowAway.BlowAwayProtocol(playerDirection, blowAwayPower);
        _anim.TakeDamageAnim();
    }

    public void ShootTakeDamage(Vector3 bulletDirection,float damage,float blowAwayPower)
    {
        Debug.Log("射撃喰らった");
        _blowAway.BlowAwayProtocol(bulletDirection, blowAwayPower);
        _anim.TakeDamageAnim();

    }
}
