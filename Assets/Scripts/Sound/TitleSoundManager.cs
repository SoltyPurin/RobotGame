using UnityEngine;

public class TitleSoundManager : MonoBehaviour
{
    private AudioSource _audioSource = default;
    private TitleBGMFade _bgmFade = default;
    [SerializeField, Header("発進音")]
    private AudioClip _scrambleSE = default;
    [SerializeField,Header("機動音")]
    private AudioClip _robotStartSE = default;
    [SerializeField,Header("ボタン移動の音")]
    private AudioClip _buttonMoveSE = default;
    [SerializeField,Header("ボタンを押した時の音")]
    private AudioClip _buttonTapSE = default;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _bgmFade = GetComponentInChildren<TitleBGMFade>();
    }

    public void PlayButtonMoveSE()
    {
        _audioSource.PlayOneShot(_buttonMoveSE);
    }
    public void PlayButtonTapSE()
    {
        _audioSource.PlayOneShot(_buttonTapSE);
    }
    public void PlayRobotStartSE()
    {
        _bgmFade.StartFadeOut();
        _audioSource.PlayOneShot(_robotStartSE);
    }
    public void PlayScrambleSound()
    {
        _audioSource.PlayOneShot(_scrambleSE);
    }
}
