using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    [SerializeField, Header("ê≥ñ Ç©ÇÁãÚÇÁÇ¡ÇΩéûÇÃImage")]
    private Image _frontDamage = default;
    [SerializeField, Header("âEÇ©ÇÁãÚÇÁÇ¡ÇΩéûÇÃImage")]
    private Image _rightDamage = default;
    [SerializeField, Header("ç∂Ç©ÇÁãÚÇÁÇ¡ÇΩéûÇÃImage")]
    private Image _leftDamage = default;

    private void Start()
    {
        _frontDamage.color = Color.clear;
        _rightDamage.color = Color.clear;
        _leftDamage.color = Color.clear;
    }

    private void Update()
    {
        _frontDamage.color = Color.Lerp(_frontDamage.color, Color.clear, Time.deltaTime);
        _rightDamage.color = Color.Lerp(_rightDamage.color,Color.clear, Time.deltaTime);
        _leftDamage.color = Color.Lerp(_leftDamage.color, Color.clear, Time.deltaTime); 
    }

    public void FrontDamage()
    {
        _frontDamage.color = new Color(0.7f, 0, 0, 0.7f);
    }
    public void RightDamage()
    {
        _rightDamage.color = new Color(0.7f, 0, 0, 0.7f);
    }
    public void LeftDamage()
    {
        _leftDamage.color = new Color(0.7f, 0, 0, 0.7f);
    }
}
