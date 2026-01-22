using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Cinemachine;
using System.Collections;

public class TitleCanvasInput : MonoBehaviour
{
    [SerializeField, Header("ゲームスタートボタン")]
    private Button _gameStart = default;
    [SerializeField, Header("ロボット調整ボタン")]
    private Button _settingButton = default;
    [SerializeField,Header("終了ボタン")]
    private Button _exitButton = default;
    [SerializeField, Header("その時のキャンバス")]
    private GameObject _currentCanvas = default;
    [SerializeField,Header("次のキャンバス")]
    private GameObject _nextCanvas = default;
    [SerializeField, Header("タイトルカメラ")]
    private CinemachineCamera _titleCamera = default;
    [SerializeField,Header("設定画面のカメラ")]
    private CinemachineCamera _settingCamera = default;
    [SerializeField,Header("出撃カメラ")]
    private CinemachineCamera _scrambleCamera = default;
    [SerializeField, Header("アニメーターコントローラー")]
    private Animator _animator = default;
    [SerializeField, Header("フェードのイメージ画像")]
    private Image _fadeImage = default;

    private CanvasSwitcher _switcher = default;
    private CameraSwitchScript _cameraSwitch = default;
    private LoadManager _loadManager = default;
    
    private void Start()
    {
        _switcher = GetComponent<CanvasSwitcher>();
        _cameraSwitch = GetComponent<CameraSwitchScript>();
        _loadManager = GameObject.FindAnyObjectByType<LoadManager>();
        _gameStart.onClick.AddListener(GameStart);
        _settingButton.onClick.AddListener(OpenSetting);
        _exitButton.onClick.AddListener(GameExit);
    }

    private void GameStart()
    {
        _animator.SetTrigger("ScrambleWait");
        _titleCamera.Priority = 0;
        _scrambleCamera.Priority = 1;
        CanvasGroup titleCanvas = _currentCanvas.GetComponent<CanvasGroup>();
        titleCanvas.alpha = 0;
        titleCanvas.blocksRaycasts = false;
        titleCanvas.interactable = false;
        StartCoroutine(ScrambleStart());
    }

    private IEnumerator ScrambleStart()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger("Scramble");
        yield return new WaitForSeconds(2f);
        StartCoroutine(ScrambleFade());
    }

    private IEnumerator ScrambleFade()
    {
        var color = _fadeImage.color;
        for(int i =0; i < 255; i++)
        {
            yield return null;
            color.a += 0.01f;
            _fadeImage.color = color;
        }

        _loadManager.StartLoad();
    }
    private void OpenSetting()
    {
        _switcher.StuckIn(_currentCanvas,_nextCanvas);
        _cameraSwitch.StuckIn(_titleCamera, _settingCamera);
    }

    private void GameExit()
    {
        Application.Quit();
    }
}
