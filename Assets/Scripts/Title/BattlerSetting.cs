using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class BattlerSetting : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のセッティングボタン")]
    private Button _meleeSettingButton = default;
    [SerializeField,Header("射撃のセッティングボタン")]
    private Button _shotWeaponSettingButton = default;
    [SerializeField,Header("バーストの選択ボタン")]
    private Button _burstSettingButton = default;
    [SerializeField, Header("現在のキャンバス")]
    private GameObject _currentCanvas = default;
    [SerializeField,Header("近接攻撃のキャンバス")]
    private GameObject _meleeCanvas = default;
    [SerializeField,Header("射撃のキャンバス")]
    private GameObject _shotWeaponCanvas = default;
    [SerializeField,Header("バーストのキャンバス")]
    private GameObject _burstCanvas = default;
    [SerializeField, Header("オプション画面のカメラ")]
    private CinemachineCamera _optionCamera = default;
    [SerializeField, Header("近接のカメラ")]
    private CinemachineCamera _meleeCamera = default;
    [SerializeField, Header("射撃のカメラ")]
    private CinemachineCamera _shotWeaponCamera = default;
    [SerializeField,Header("バーストのカメラ")]
    private CinemachineCamera _burstCamera = default;

    private CameraSwitchScript _cameraSwitch = default;
    private CanvasSwitcher _switcher = default;
    private TitleSoundManager _sound = default;
    private void Start()
    {
        _switcher = GetComponent<CanvasSwitcher>();
        _cameraSwitch = GetComponent<CameraSwitchScript>();
        _sound = FindAnyObjectByType<TitleSoundManager>();
        _meleeSettingButton.onClick.AddListener(InMeleeSettingProtocol);
        _shotWeaponSettingButton.onClick.AddListener(InShotWeaponProtocol);
        _burstSettingButton.onClick.AddListener(InBurstSettingProtocol);
}

    private void InMeleeSettingProtocol()
    {
        _switcher.StuckIn(_currentCanvas, _meleeCanvas);
        _cameraSwitch.StuckIn(_optionCamera, _meleeCamera);
        _sound.PlayButtonTapSE();
    }

    private void InShotWeaponProtocol()
    {
        _switcher.StuckIn(_currentCanvas,_shotWeaponCanvas);
        _cameraSwitch.StuckIn(_optionCamera, _shotWeaponCamera);
        _sound.PlayButtonTapSE();
    }

    private void InBurstSettingProtocol()
    {
        _switcher.StuckIn(_currentCanvas,_burstCanvas);
        _cameraSwitch.StuckIn(_optionCamera, _burstCamera);
        _sound.PlayButtonTapSE();
    }
}
