using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Cinemachine;

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

    private CanvasSwitcher _switcher = default;
    private CameraSwitchScript _cameraSwitch = default;
    
    private void Start()
    {
        _switcher = GetComponent<CanvasSwitcher>();
        _cameraSwitch = GetComponent<CameraSwitchScript>();
        _gameStart.onClick.AddListener(GameStart);
        _settingButton.onClick.AddListener(OpenSetting);
        _exitButton.onClick.AddListener(GameExit);
    }

    private void GameStart()
    {
        SceneManager.LoadScene("Honpen");
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
