using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UniRx;

public class TakeDamageScript : MonoBehaviour
{
    [SerializeField, Header("ãﬂê⁄çUåÇÇÃçdíºéûä‘")]
    private float _meleeBlowAwayTime = 1;
    [SerializeField, Header("éÀåÇçUåÇÇÃçdíºéûä‘")]
    private float _shootBlowAwayTime = 0.3f;
    [SerializeField, Header("ç≈èâÇÃHP")]
    protected int _userHP = 600;
    public int UserHP
    {
        get { return _userHP; }
    }

    protected DeathManager _deathManager = default;
    private BlowAway _blowAway = default;
    protected PlayAnimationScript _anim = default;

    private bool _isBlowning = false;
    public bool IsBlowning
    {
        get { return _isBlowning; }
    }
    public virtual void Start()
    {
        _deathManager = FindAnyObjectByType<DeathManager>();
        _blowAway = GetComponent<BlowAway>();
        _anim = GetComponent<PlayAnimationScript>();
    }

    public virtual void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        _isBlowning = true;
        _blowAway.BlowAwayProtocol(attackDirection, blowAwayPower);
        _anim.TakeDamageAnim();
        StartCoroutine(ReleaseBlowAway(_meleeBlowAwayTime));
        _userHP -= damage;
    }

    public virtual void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        _isBlowning = true;
        _blowAway.BlowAwayProtocol(bulletDirection, blowAwayPower);
        _anim.TakeDamageAnim();
        StartCoroutine(ReleaseBlowAway(_shootBlowAwayTime));
        _userHP -= damage;


    }

    private IEnumerator ReleaseBlowAway(float blowingTime)
    {
        yield return new WaitForSeconds(blowingTime);
        _isBlowning = false;

    }

    public virtual void DeathCheck()
    {
        if (_userHP <= 0)
        {
            _anim.DeathAnim();
        }
    }
    }
