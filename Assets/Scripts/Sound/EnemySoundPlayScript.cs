using UnityEngine;

public class EnemySoundPlayScript : MonoBehaviour
{
    private AudioSource _source = default;

    [SerializeField, Header("‹ßÚ‚Ì”í’e‰¹")]
    private AudioClip _meleeTakeDamage = default;
    [SerializeField, Header("ËŒ‚‚Ì”í’e‰¹")]
    private AudioClip _shotTakeDamage = default;

    [SerializeField,Header("aŒ‚‰¹")]
    private AudioClip _meleeSE = default;
    [SerializeField,Header("ShootSE")]
    private AudioClip _shotSE = default;

    [SerializeField,Header("€–S‚Ì”š”­‰¹")]
    private AudioClip _deathExplosionSE = default;   

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    public void PlayMeleeSE()
    {
        _source.PlayOneShot(_meleeSE);
    }
    public void PlayShotSE()
    {
        _source.PlayOneShot(_shotSE);
    }
    public void PlayTakeMeleeDamage()
    {
        _source.PlayOneShot(_meleeTakeDamage);
    }

    public void PlayeTakeShotDamage()
    {
        _source.PlayOneShot(_shotTakeDamage);
    }

    public void PlayDeathExplosionSE()
    {
        _source.PlayOneShot(_deathExplosionSE);
    }
}
