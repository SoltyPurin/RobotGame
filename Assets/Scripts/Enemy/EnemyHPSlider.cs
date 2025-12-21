using UnityEngine;
using UnityEngine.UI;

public class EnemyHPSlider : MonoBehaviour
{
    [SerializeField, Header("HPのバー")]
    private Slider _hpSlider = default;
    [SerializeField, Header("キャンバス")]
    private GameObject _canvas = default;

    private GameObject _player = default;

    public void Initialize(int hp)
    {
        _hpSlider.maxValue = hp;
        _hpSlider.value = hp;
        _player = GameObject.FindWithTag("Player");
    }

    public void ValueUpdate(int hp)
    {
        _hpSlider.value = hp;
    }

    private void Update()
    {
        _canvas.transform.LookAt(_player.transform.position);
    }
}
