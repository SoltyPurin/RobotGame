using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    [SerializeField, Header("–ß‚éƒ{ƒ^ƒ“")]
    private Button _returnButton = default;

    private CanvasSwitcher _switcher = default;
    private CameraSwitchScript _cameraSwitch = default;

    private TitleSoundManager _sound = default;
    private void Start()
    {
        _switcher = GetComponent<CanvasSwitcher>();
        _cameraSwitch = GetComponent<CameraSwitchScript>();
        _sound = FindAnyObjectByType<TitleSoundManager>();
        _returnButton.onClick.AddListener(ReturnProtocol);
    }

    private void ReturnProtocol()
    {
        _sound.PlayButtonTapSE();
        _switcher.StuckOut();
        _cameraSwitch.StuckOut();
    }
}
