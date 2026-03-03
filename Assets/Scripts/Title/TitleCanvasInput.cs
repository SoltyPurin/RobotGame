using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Collections;

public class TitleCanvasInput : MonoBehaviour
{
    [SerializeField, Header("出撃画面移行ボタン")]
    private Button _openScrambleButton = default;
    [SerializeField, Header("ロボット調整ボタン")]
    private Button _settingButton = default;
    [SerializeField,Header("スタートボタン")]
    private Button _startButton = default;
    [SerializeField, Header("練習場のボタン")]
    private Button _practiceButton = default;
    [SerializeField,Header("終了ボタン")]
    private Button _exitButton = default;
    [SerializeField, Header("その時のキャンバス")]
    private GameObject _currentCanvas = default;
    [SerializeField, Header("戻るボタンがあるキャンバス")]
    private GameObject _returnButtonCanvas = default;
    [SerializeField, Header("出撃画面のキャンバス")]
    private GameObject _scrambleCanvas = default;
    [SerializeField,Header("設定画面のキャンバス")]
    private GameObject _assembleCanvas = default;
    [SerializeField, Header("タイトルカメラ")]
    private CinemachineCamera _titleCamera = default;
    [SerializeField,Header("設定画面のカメラ")]
    private CinemachineCamera _settingCamera = default;
    [SerializeField,Header("出撃カメラ")]
    private CinemachineCamera _scrambleCamera = default;

    private CanvasSwitcher _switcher = default;
    private CameraSwitchScript _cameraSwitch = default;
    private ScramblePlayer _scramblePlayer = default;
    private TitleSoundManager _sound = default;

    private void Start()
    {
        Time.timeScale = 1;
        _switcher = GetComponent<CanvasSwitcher>();
        _cameraSwitch = GetComponent<CameraSwitchScript>();
        _scramblePlayer = GetComponent<ScramblePlayer>();
        _sound = FindAnyObjectByType<TitleSoundManager>();
        _openScrambleButton.onClick.AddListener(OpenScrambleCanvas);
        _settingButton.onClick.AddListener(OpenSetting);
        _startButton.onClick.AddListener(GameStart);
        _practiceButton.onClick.AddListener(PracticeStart);
        _exitButton.onClick.AddListener(GameExit);
    }

    private void OpenScrambleCanvas()
    {
        _sound.PlayButtonTapSE();
        _switcher.StuckIn(_currentCanvas,_scrambleCanvas);
        _cameraSwitch.StuckIn(_titleCamera,_settingCamera);
    }

    private void OpenSetting()
    {
        _sound.PlayButtonTapSE();
        _switcher.StuckIn(_scrambleCanvas, _assembleCanvas);
        //_cameraSwitch.StuckIn(_titleCamera, _settingCamera);
    }

    private void GameStart()
    {
        _sound.PlayButtonTapSE();
        _scramblePlayer.PlayScrambleAnim(_settingCamera, _scrambleCamera, _scrambleCanvas,_returnButtonCanvas,"Honpen");
    }

    private void PracticeStart()
    {
        _sound.PlayButtonTapSE();
        _scramblePlayer.PlayScrambleAnim(_settingCamera, _scrambleCamera, _scrambleCanvas, _returnButtonCanvas, "Practice");

    }

    private void GameExit()
    {
        _sound.PlayButtonTapSE();
        Application.Quit();
    }
}
