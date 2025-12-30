using UnityEngine;

public class PlayerSoundPlayScript : MonoBehaviour
{
    private AudioSource _audioSource = default;

    [SerializeField, Header("ËŒ‚‰¹")]
    private AudioClip _shootSound = default;
    [SerializeField,Header("Œ•‚ğU‚é‰¹")]
    private AudioClip _swordSwingSound = default;

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
}
