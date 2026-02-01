using UnityEngine;

public class TitleBGMFade : MonoBehaviour
{
    private AudioSource _audio = default;

    private bool _isFade = false;
    private bool _isFadeOut = false;
    private float _fadeOutSeconds = 5;
    private float _fadeDeltaTime = 0;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
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

        _fadeDeltaTime += Time.deltaTime;
        if(_fadeDeltaTime >= _fadeOutSeconds)
        {
            _fadeDeltaTime = _fadeOutSeconds;
            _isFadeOut = false;
        }
        _audio.volume = (1 - _fadeDeltaTime / _fadeOutSeconds);
    }
}
