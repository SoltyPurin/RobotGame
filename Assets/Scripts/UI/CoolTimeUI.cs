using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUI : MonoBehaviour
{
    private Slider _slider = default;
    private PlayerInputManager _inputManager = default;

    private bool _canCountCoolTime = false; 

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _inputManager = FindAnyObjectByType<PlayerInputManager>();
        _slider.maxValue = _inputManager.ShootCoolTime;
    }

    private void Update()
    {
        _slider.value = _inputManager.CurrentCoolTime;
    }

}
