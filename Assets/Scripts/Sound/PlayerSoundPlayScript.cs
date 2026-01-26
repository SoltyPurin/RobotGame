using UnityEngine;

public class PlayerSoundPlayScript : MonoBehaviour
{
    private AudioSource _audioSource = default;

    [SerializeField, Header("ËŒ‚‰¹")]
    private AudioClip _shootSound = default;
    [SerializeField,Header("Œ•‚ğU‚é‰¹")]
    private AudioClip _swordSwingSound = default;
    [SerializeField,Header("‹ßÚ”í’e‰¹")]
    private AudioClip _meleeTakeDamage = default;
    [SerializeField, Header("ËŒ‚”í’e‰¹")]
    private AudioClip _shotTakeDamage = default;
    [SerializeField,Header("€–S‚Ì”š”­‰¹")]
    private AudioClip _deathExplosion = default;
    [SerializeField, Header("ƒ_ƒbƒVƒ…‚Ì‰¹")]
    private AudioClip _dashSE = default;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayShootSound()
    {
        _audioSource.PlayOneShot(_shootSound);
    }

    public void PlaySwordSwing()
    {
        _audioSource.PlayOneShot(_swordSwingSound);
    }

    public void PlayMeleeTakeDamage()
    {
        _audioSource.PlayOneShot(_meleeTakeDamage);
    }

    public void PlayShotTakeDamage()
    {
        _audioSource.PlayOneShot(_shotTakeDamage);
    }

    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(_deathExplosion);
    }

    public void PlayDashSound(bool isRunning)
    {
        if (isRunning)
        {
            _audioSource.Stop();
        }
        else
        {
            _audioSource.PlayOneShot(_dashSE);
        }
    }
}
