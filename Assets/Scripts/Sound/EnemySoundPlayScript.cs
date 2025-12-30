using UnityEngine;

public class EnemySoundPlayScript : MonoBehaviour
{
    private AudioSource _source = default;

    [SerializeField, Header("‹ßÚ‚Ì”í’e‰¹")]
    private AudioClip _meleeTakeDamage = default;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayTakeMeleeDamage()
    {
        _source.PlayOneShot(_meleeTakeDamage);
    }
}
