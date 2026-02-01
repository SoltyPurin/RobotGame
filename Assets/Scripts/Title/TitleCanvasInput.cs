using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        _gameStart.onClick.AddListener(GameStart);
        _settingButton.onClick.AddListener(OpenSetting);
        _exitButton.onClick.AddListener(GameExit);
    }

    private void GameStart()
    {
        _sound.PlayButtonTapSE();
        _scramblePlayer.PlayScrambleAnim(_titleCamera,_scrambleCamera,_currentCanvas);
    }

    private void OpenSetting()
    {
        _sound.PlayButtonTapSE();
        _switcher.StuckIn(_currentCanvas,_nextCanvas);
        _cameraSwitch.StuckIn(_titleCamera, _settingCamera);
    }

    private void GameExit()
    {
        _sound.PlayButtonTapSE();
        Application.Quit();
    }
}
