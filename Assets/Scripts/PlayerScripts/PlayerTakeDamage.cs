using Unity.Cinemachine;
using UnityEngine;

public class PlayerTakeDamage : TakeDamageScript
{
    [SerializeField, Header("近接攻撃食らった時の揺れ")]
    private float _meleeDuration = 1;
    [SerializeField, Header("射撃攻撃食らった時の揺れ")]
    private float _shootDuration = 0.5f;
    [SerializeField, Header("ロックオンカメラ")]
    private CinemachineCamera _lockOnCamera = default;


    private UIViewer _uiViewer = default;
    private PlayerInputManager _input = default;
    private CinemachineImpulseSource _shake = default;

    public override void Start()
    {
        base.Start();
        _shake = GetComponent<CinemachineImpulseSource>();
        _uiViewer = FindAnyObjectByType<UIViewer>();
        _input = GetComponent<PlayerInputManager>();
        _uiViewer.SetHealth(_userHP);
    }

    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            Debug.Log("近接攻撃食らった");
            _shake.GenerateImpulseWithForce(_meleeDuration);
        }
        _uiViewer.SetHealth(_userHP);
        DeathCheck();
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_shootDuration);

        }
        _uiViewer.SetHealth(_userHP);
        DeathCheck();
    }

    public override void DeathCheck()
    {
        base.DeathCheck();
        Debug.Log("プレイヤーの体力は" + _userHP);
        if(_userHP <= 0)
        {
            _lockOnCamera.LookAt = transform;
            _input.Dead();
            _deathManager.PlayerCheckHP(_userHP);
        }
    }
}
