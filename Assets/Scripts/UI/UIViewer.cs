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
}