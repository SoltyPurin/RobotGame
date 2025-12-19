using Unity.Cinemachine;
using UnityEngine;

public class PlayerTakeDamage : TakeDamageScript
{
    [SerializeField, Header("‹ßÚUŒ‚H‚ç‚Á‚½‚Ì—h‚ê")]
    private float _meleeDuration = 1;
    [SerializeField, Header("ËŒ‚UŒ‚H‚ç‚Á‚½‚Ì—h‚ê")]
    private float _shootDuration = 0.5f;

    private UIViewer _uiViewer = default;
    private CinemachineImpulseSource _shake = default;

    public override void Start()
    {
        base.Start();
        _shake = GetComponent<CinemachineImpulseSource>();
        _uiViewer = FindAnyObjectByType<UIViewer>();
        _uiViewer.SetHealth(_userHP);
    }

    public override void MeleeTakeDamage(Vector3 attackDirection, int damage, float blowAwayPower)
    {
        base.MeleeTakeDamage(attackDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            Debug.Log("‹ßÚUŒ‚H‚ç‚Á‚½");
            _shake.GenerateImpulseWithForce(_meleeDuration);
        }
        _deathManager.PlayerCheckHP(_userHP);
        _uiViewer.SetHealth(_userHP);
    }

    public override void ShootTakeDamage(Vector3 bulletDirection, int damage, float blowAwayPower)
    {
        base.ShootTakeDamage(bulletDirection, damage, blowAwayPower);
        if (_shake != null)
        {
            _shake.GenerateImpulseWithForce(_shootDuration);

        }
        _deathManager.PlayerCheckHP(_userHP);
        _uiViewer.SetHealth(_userHP);

    }
}
