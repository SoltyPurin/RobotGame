using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using Unity.Cinemachine;
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
    [SerializeField, Header("近接攻撃食らった時の揺れ")]
    private float _meleeDuration = 1;
    [SerializeField, Header("射撃攻撃食らった時の揺れ")]
    private float _shootDuration = 0.5f;

    private CinemachineImpulseSource _shake = default;


    private bool _isBlowning = false;
    public bool IsBlowning
    {
        get { return _isBlowning; }
    }



    private void Start()
    {
        _shake = GetComponent<CinemachineImpulseSource>();
    }
    public void MeleeTakeDamage(Vector3 attackDirection,float damage,float blowAwayPower)
    {
        _isBlowning = true;
         _blowAway.BlowAwayProtocol(attackDirection, blowAwayPower);
        _anim.TakeDamageAnim();
        StartCoroutine(ReleaseBlowAway(_meleeBlowAwayTime));
        if(_shake != null)
        {
            Debug.Log("近接攻撃食らった");
            _shake.GenerateImpulseWithForce(_meleeDuration);
        }
    }

    public void ShootTakeDamage(Vector3 bulletDirection,float damage,float blowAwayPower)
    {
        _isBlowning = true;
        _blowAway.BlowAwayProtocol(bulletDirection, blowAwayPower);
        _anim.TakeDamageAnim();
        StartCoroutine(ReleaseBlowAway(_shootBlowAwayTime));
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_shootDuration);
        }
    }

    private IEnumerator ReleaseBlowAway(float blowingTime)
    {
        yield return new WaitForSeconds(blowingTime);
        _isBlowning = false;

    }
}
