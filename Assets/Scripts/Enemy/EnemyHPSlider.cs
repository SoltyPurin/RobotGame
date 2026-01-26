using UnityEngine;
using UnityEngine.UI;

public class EnemyHPSlider : MonoBehaviour
{
    [SerializeField, Header("HPのバー")]
    private Slider _hpSlider = default;
    [SerializeField, Header("キャンバス")]
    private GameObject _canvas = default;

    private GameObject _player = default;
    private bool _isInitialized = false;

    public void Initialize(int hp)
    {
        _hpSlider.maxValue = hp;
        _hpSlider.value = hp;
        _player = GameObject.FindWithTag("Player");
        _isInitialized = true;
    }

    public void ValueUpdate(int hp)
    {
        _hpSlider.value = hp;
    }

    private void Update()
    {
        if(!_isInitialized)
        {
            return;
        }
        _canvas.transform.LookAt(_player.transform.position);
    }
}
