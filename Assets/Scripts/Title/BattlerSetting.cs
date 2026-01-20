using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class BattlerSetting : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のセッティングボタン")]
    private Button _meleeSettingButton = default;
    [SerializeField,Header("射撃のセッティングボタン")]
    private Button _shotWeaponSettingButton = default;
    [SerializeField, Header("現在のキャンバス")]
    private GameObject _currentCanvas = default;
    [SerializeField,Header("近接攻撃のキャンバス")]
    private GameObject _meleeCanvas = default;
    [SerializeField,Header("射撃のキャンバス")]
    private GameObject _shotWeaponCanvas = default;
    [SerializeField, Header("オプション画面のカメラ")]
    private CinemachineCamera _optionCamera = default;
    [SerializeField, Header("近接のカメラ")]
    private CinemachineCamera _meleeCamera = default;
    [SerializeField, Header("射撃のカメラ")]
    private CinemachineCamera _shotWeaponCamera = default;

    private CameraSwitchScript _cameraSwitch = default;
    private CanvasSwitcher _switcher = default;
    private void Start()
    {
        _switcher = GetComponent<CanvasSwitcher>();
        _cameraSwitch = GetComponent<CameraSwitchScript>();
        _meleeSettingButton.onClick.AddListener(InMeleeSettingProtocol);
        _shotWeaponSettingButton.onClick.AddListener(InShotWeaponProtocol);
}

    private void InMeleeSettingProtocol()
    {
        _switcher.StuckIn(_currentCanvas, _meleeCanvas);
        _cameraSwitch.StuckIn(_optionCamera, _meleeCamera);
    }

    private void InShotWeaponProtocol()
    {
        _switcher.StuckIn(_currentCanvas,_shotWeaponCanvas);
        _cameraSwitch.StuckIn(_optionCamera, _shotWeaponCamera);
    }
}
