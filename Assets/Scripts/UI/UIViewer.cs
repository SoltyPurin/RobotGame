using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewer : MonoBehaviour
{
    [SerializeField, Header("体力")]
    private TextMeshProUGUI _health;
    [SerializeField, Header("ダッシュ時間のスライダー")]
    private Slider _dashTimeSlider = default;
    [SerializeField, Header("射撃のクールタイムのスライダー")]
    private Slider _shotWeaponCoolTimeSlider = default;
    [SerializeField, Header("ダッシュ時のエフェクト")]
    private GameObject _dashEffect = default;
    [SerializeField, Header("GOの文字")]
    private GameObject _goText = default;
    [SerializeField, Header("キャンバスグループ")]
    private CanvasGroup _playerUI = default;
    [SerializeField]
    private CanvasGroup _performanceCanvas = default;
    [SerializeField]
    private CanvasGroup _pauseMenu = default;

    public void SetShotWeaponMax(float max)
    {
        _shotWeaponCoolTimeSlider.maxValue = max;
    }
    public void SetShotWeaponValue(float value)
    {
        _shotWeaponCoolTimeSlider.value = value;
    }
    public void SwitchDashEffect(bool isRunning)
    {
        _dashEffect.SetActive(isRunning);
    }
    public void SetDashTimeSliderMax(float max)
    {
        _dashTimeSlider.maxValue = max;
    }
    public void SetDashValue(float value) 
    {
        _dashTimeSlider.value = value;
    }

    public void SetHealth(int health)
    {
        _health.text = health.ToString();
    }

    public void OnlineGoMark(bool isTrue)
    {
        if (isTrue)
        {
            _goText.SetActive(true);
        }
        else
        {
           _goText.SetActive(false);
        }
    }

    public void BurstStart()
    {
        _playerUI.alpha = 0;
        _performanceCanvas.alpha = 1;
    }

    public void BurstEnd()
    {
        _playerUI.alpha = 1;
        _performanceCanvas.alpha = 0;
    }

    public void Pause()
    {
        _playerUI.alpha = 0;
        _playerUI.blocksRaycasts = false;
        _performanceCanvas.alpha = 0;
        _performanceCanvas.blocksRaycasts = false;
        _pauseMenu.alpha = 1;
        _pauseMenu.interactable = true;
    }
    public void PauseBreak()
    {
        _playerUI.alpha = 1;
        _pauseMenu.alpha = 0;
        _pauseMenu.interactable = false ;
    }
}