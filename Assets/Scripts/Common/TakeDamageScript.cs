using UnityEngine;

public class TakeDamageScript : MonoBehaviour
{
    [SerializeField, Header("吹き飛びのスクリプト")]
    private BlowAway _blowAway = default;
    [SerializeField, Header("アニメーション再生のスクリプト")]
    private PlayAnimationScript _anim = default;

    private bool _isBlowning = false;
    public bool IsBlowning
    {
        get { return _isBlowning; }
    }
    public void MeleeTakeDamage(Vector3 attackDirection,float damage,float blowAwayPower)
    {
        _isBlowning = true;
        Debug.Log("近接攻撃喰らった");
         _blowAway.BlowAwayProtocol(attackDirection, blowAwayPower);
        _anim.TakeDamageAnim();
    }

    public void ShootTakeDamage(Vector3 bulletDirection,float damage,float blowAwayPower)
    {
        _isBlowning = true;
        Debug.Log("射撃喰らった");
        _blowAway.BlowAwayProtocol(bulletDirection, blowAwayPower);
        _anim.TakeDamageAnim();

    }

    public void EndBlowAway()
    {
        _isBlowning = false;
    }
}
