using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.Cinemachine;
using UnityEngine.Playables;

public class ScramblePlayer : MonoBehaviour
{
    [SerializeField, Header("アニメーターコントローラー")]
    private Animator _animator = default;
    [SerializeField, Header("フェードのイメージ画像")]
    private Image _fadeImage = default;
    [SerializeField,Header("出撃タイムライン")]
    private PlayableDirector _playableDirector = null;

    private LoadManager _loadManager = default;
    private TitleSoundManager _soundManager = default;

    private void Start()
    {
        _loadManager = FindAnyObjectByType<LoadManager>();
        _soundManager = FindAnyObjectByType<TitleSoundManager>();

    }
    public void PlayScrambleAnim(CinemachineCamera titleCamera,CinemachineCamera scrambleCamera,
        GameObject currentCanvas,GameObject returnButtonCanvas, string sceneName)
    {
        _playableDirector.Play();
        _animator.SetTrigger("ScrambleWait");
        titleCamera.Priority = 0;
        scrambleCamera.Priority = 1;
        CanvasGroup titleCanvas = currentCanvas.GetComponent<CanvasGroup>();
        titleCanvas.alpha = 0;
        titleCanvas.blocksRaycasts = false;
        titleCanvas.interactable = false;
        CanvasGroup returnCanvas = returnButtonCanvas.GetComponent<CanvasGroup>();
        returnCanvas.alpha = 0;
        returnCanvas.blocksRaycasts = false;
        returnCanvas.interactable = false ;
        StartCoroutine(ScrambleStart(sceneName));
    }

    private IEnumerator ScrambleStart(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        _soundManager.PlayRobotStartSE();
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(ScrambleFade(sceneName));
    }

    private IEnumerator ScrambleFade(string sceneName)
    {
        var color = _fadeImage.color;
        for (int i = 0; i < 255; i++)
        {
            yield return null;
            color.a += 0.01f;
            _fadeImage.color = color;
        }

        _loadManager.StartLoad(sceneName);
    }

}
