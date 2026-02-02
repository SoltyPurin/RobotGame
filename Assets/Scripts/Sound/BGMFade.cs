using UnityEngine;

public class BGMFade : MonoBehaviour
{
    private AudioSource _audio = default;

    private bool _isFadeOut = false;
    [SerializeField,Header("フェードアウトさせる時間")]
    private float _fadeOutSeconds = 5;
    [SerializeField, Header("フェードアウト倍率")]
    private float _fadeOutRatio = 0.1f;
    private float _fadeDeltaTime = 0;

    private float _initBGMVolume = 1;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _initBGMVolume = _audio.volume;
    }

    public void StartFadeOut()
    {
        _isFadeOut = true;
    }

    private void Update()
    {
        if( !_isFadeOut)
        {
            return;
        }

        _fadeDeltaTime += Time.deltaTime * _fadeOutRatio;
        if(_fadeDeltaTime >= _fadeOutSeconds)
        {
            _fadeDeltaTime = _fadeOutSeconds;
            _isFadeOut = false;
        }
        _audio.volume = (_initBGMVolume - _fadeDeltaTime / _fadeOutSeconds);
    }
}
