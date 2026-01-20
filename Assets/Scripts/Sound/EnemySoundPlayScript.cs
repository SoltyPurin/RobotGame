using UnityEngine;

public class EnemySoundPlayScript : MonoBehaviour
{
    private AudioSource _source = default;

    [SerializeField, Header("‹ßÚ‚Ì”í’e‰¹")]
    private AudioClip _meleeTakeDamage = default;
    [SerializeField, Header("ËŒ‚‚Ì”í’e‰¹")]
    private AudioClip _shotTakeDamage = default;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayTakeMeleeDamage()
    {
        _source.PlayOneShot(_meleeTakeDamage);
    }

    public void PlayeTakeShotDamage()
    {
        _source.PlayOneShot(_shotTakeDamage);
    }
}
