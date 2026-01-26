using UnityEngine;

public class EnemySoundPlayScript : MonoBehaviour
{
    private AudioSource _source = default;

    [SerializeField, Header("ãﬂê⁄ÇÃîÌíeâπ")]
    private AudioClip _meleeTakeDamage = default;
    [SerializeField, Header("éÀåÇÇÃîÌíeâπ")]
    private AudioClip _shotTakeDamage = default;

    [SerializeField,Header("éaåÇâπ")]
    private AudioClip _meleeSE = default;
    [SerializeField,Header("ShootSE")]
    private AudioClip _shotSE = default;

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
}
