using UnityEngine;

public class PlayerSoundPlayScript : MonoBehaviour
{
    private AudioSource _audioSource = default;

    [SerializeField, Header("射撃音")]
    private AudioClip _shootSound = default;
    [SerializeField,Header("剣を振る音")]
    private AudioClip _swordSwingSound = default;
    [SerializeField,Header("近接被弾音")]
    private AudioClip _meleeTakeDamage = default;
    [SerializeField, Header("射撃被弾音")]
    private AudioClip _shotTakeDamage = default;
    [SerializeField,Header("死亡時の爆発音")]
    private AudioClip _deathExplosion = default;
    [SerializeField, Header("ダッシュ時の音")]
    private AudioClip _dashSE = default;
    [SerializeField,Header("バースト準備完了の音")]
    private AudioClip _burstReadySound = default;

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

    public void PlayBurstReadySound()
    {
        _audioSource.volume = 1;
        _audioSource.PlayOneShot(_burstReadySound);
        _audioSource.volume = 0.2f;
    }

}
