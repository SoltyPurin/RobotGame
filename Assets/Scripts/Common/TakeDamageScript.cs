using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class TakeDamageScript : MonoBehaviour
{
    [SerializeField, Header("吹き飛びのスクリプト")]
    private BlowAway _blowAway = default;
    [SerializeField, Header("アニメーション再生のスクリプト")]
    private PlayAnimationScript _anim = default;
    [SerializeField, Header("近接攻撃の硬直時間")]
    private float _meleeBlowAwayTime = 1;
    [SerializeField, Header("射撃攻撃の硬直時間")]
    private float _shootBlowAwayTime = 0.3f;

    private CameraShake _shake = default;


    private bool _isBlowning = false;
    public bool IsBlowning
    {
        get { return _isBlowning; }
    }

    private void Start()
    {
        _shake = GameObject.FindFirstObjectByType<CameraShake>();
    }
    public void MeleeTakeDamage(Vector3 attackDirection,float damage,float blowAwayPower)
    {
        _isBlowning = true;
         _blowAway.BlowAwayProtocol(attackDirection, blowAwayPower);
        _anim.TakeDamageAnim();
        StartCoroutine(ReleaseBlowAway(_meleeBlowAwayTime));
    }

    public void ShootTakeDamage(Vector3 bulletDirection,float damage,float blowAwayPower)
    {
        _isBlowning = true;
        _blowAway.BlowAwayProtocol(bulletDirection, blowAwayPower);
        _anim.TakeDamageAnim();
        StartCoroutine(ReleaseBlowAway(_shootBlowAwayTime));
    }

    private IEnumerator ReleaseBlowAway(float blowingTime)
    {
        yield return new WaitForSeconds(blowingTime);
        _isBlowning = false;

    }
}
