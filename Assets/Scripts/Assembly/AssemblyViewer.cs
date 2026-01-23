using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AssemblyViewer : MonoBehaviour
{
    [SerializeField, Header("戻るボタン")]
    private Button _returnButton = default;

    [SerializeField, Header("近接攻撃力")]
    private Slider _meleeAttackPower = default;
    [SerializeField,Header("突進速度")]
    private Slider _meleeRushSpeed = default;
    [SerializeField,Header("吹き飛ばし力")]
    private Slider _meleeBlowAwayPower = default;
    [SerializeField, Header("射撃攻撃力")]
    private Slider _shotAttackPower = default;
    [SerializeField,Header("クールタイム")]
    private Slider _coolTime = default;
    [SerializeField,Header("弾速")]
    private Slider _bulletSpeed = default;


    [SerializeField, Header("近接攻撃力")]
    private TextMeshProUGUI _meleeAttackPowerText = default;
    [SerializeField, Header("突進速度")]
    private TextMeshProUGUI _meleeRushSpeedText = default;
    [SerializeField, Header("吹き飛ばし力")]
    private TextMeshProUGUI _meleeBlowAwayText = default;
    [SerializeField,Header("射撃攻撃力")]
    private TextMeshProUGUI _shotAttackPowerText = default;
    [SerializeField,Header("クールタイム")]
    private TextMeshProUGUI _coolTimeText = default;
    [SerializeField,Header("弾速")]
    private TextMeshProUGUI _bulletSpeedText = default;

    [SerializeField, Header("近接の振り分けポイント")]
    private TextMeshProUGUI _meleeRemainText = default;
    [SerializeField, Header("射撃の振り分けポイント")]
    private TextMeshProUGUI _shotWeaponRemain = default;

    private AssemblyPointDispatcher _dispatcher = default;

    public void RemainStartSet(int melee,int shot)
    {
        _meleeRemainText.text = melee.ToString();
        _shotWeaponRemain.text = shot.ToString();
    }
    private void Start()
    {
        _dispatcher = GetComponent<AssemblyPointDispatcher>();

        _meleeAttackPower.SetValueWithoutNotify(PlayerPrefs.GetInt(AssemblyPointDispatcher.MeleePower));
        _meleeRushSpeed.SetValueWithoutNotify(PlayerPrefs.GetInt(AssemblyPointDispatcher.MeleeRushSpeed));
        _meleeBlowAwayPower.SetValueWithoutNotify(PlayerPrefs.GetInt(AssemblyPointDispatcher.MeleeBlowAway));
        _meleeAttackPowerText.text = _meleeAttackPower.value.ToString();
        _meleeRushSpeedText.text = _meleeRushSpeed.value.ToString();
        _meleeBlowAwayText.text = _meleeBlowAwayPower.value.ToString();


        _shotAttackPower.SetValueWithoutNotify(PlayerPrefs.GetInt(AssemblyPointDispatcher.ShotWeaponPower));
        _coolTime.SetValueWithoutNotify(PlayerPrefs.GetInt(AssemblyPointDispatcher.CoolTime));
        _bulletSpeed.SetValueWithoutNotify(PlayerPrefs.GetInt(AssemblyPointDispatcher.BulletSpeed));
        _shotAttackPowerText.text = _shotAttackPower.value.ToString();
        _coolTimeText.text = _coolTime.value.ToString();
        _bulletSpeedText.text = _bulletSpeed.value.ToString();


        _returnButton.onClick.AddListener(_dispatcher.SaveStatus);

        _meleeAttackPower.onValueChanged.AddListener(v =>
            _dispatcher.ValueChange(v, ParamName.MeleeAttackPower, true));

        _meleeRushSpeed.onValueChanged.AddListener(v =>
            _dispatcher.ValueChange(v, ParamName.MeleeRushSpeed, true));

        _meleeBlowAwayPower.onValueChanged.AddListener(v =>
            _dispatcher.ValueChange(v, ParamName.MeleeBlowAway, true));

        _shotAttackPower.onValueChanged.AddListener(v =>
            _dispatcher.ValueChange(v, ParamName.ShotWeaponAttackPower, false));

        _coolTime.onValueChanged.AddListener(v =>
            _dispatcher.ValueChange(v, ParamName.CoolTime, false));

        _bulletSpeed.onValueChanged.AddListener(v =>
            _dispatcher.ValueChange(v, ParamName.BulletSpeed, false));
    }

    public void MeleeUpdateValue(float value,float totalMax, float attackPowerMax,float rushSpeedMax,float blowAwayMax, ParamName name)
    {
        _meleeRemainText.text = totalMax.ToString();
        switch (name)
        {
            case ParamName.MeleeAttackPower:
                _meleeAttackPower.SetValueWithoutNotify(value);
                _meleeRushSpeed.SetValueWithoutNotify(rushSpeedMax);
                _meleeBlowAwayPower.SetValueWithoutNotify(blowAwayMax);
                _meleeAttackPowerText.text = _meleeAttackPower.value.ToString();
                _meleeRushSpeedText.text = _meleeRushSpeed.value.ToString();
                _meleeBlowAwayText.text = _meleeBlowAwayPower.value.ToString();
                break;

            case ParamName.MeleeRushSpeed:
                _meleeAttackPower.SetValueWithoutNotify(attackPowerMax);
                _meleeRushSpeed.SetValueWithoutNotify(value);
                _meleeBlowAwayPower.SetValueWithoutNotify(blowAwayMax);
                _meleeAttackPowerText.text = _meleeAttackPower.value.ToString();
                _meleeRushSpeedText.text = _meleeRushSpeed.value.ToString();
                _meleeBlowAwayText.text = _meleeBlowAwayPower.value.ToString();
                break;

            case ParamName.MeleeBlowAway:
                _meleeAttackPower.SetValueWithoutNotify(attackPowerMax);
                _meleeRushSpeed.SetValueWithoutNotify(rushSpeedMax);
                _meleeBlowAwayPower.SetValueWithoutNotify(value);
                _meleeAttackPowerText.text = _meleeAttackPower.value.ToString();
                _meleeRushSpeedText.text = _meleeRushSpeed.value.ToString();
                _meleeBlowAwayText.text = _meleeBlowAwayPower.value.ToString();
                break;
        }
    }

    public void ShotUpdateValue(float value, float totalMax, float attackPowerMax, float coolTimeMax, float bulletSpeedMax, ParamName name)
    {
        _shotWeaponRemain.text = totalMax.ToString();
        switch (name)
        {
            case ParamName.ShotWeaponAttackPower:
                _shotAttackPower.SetValueWithoutNotify(value);
                _coolTime.SetValueWithoutNotify(coolTimeMax);
                _bulletSpeed.SetValueWithoutNotify(bulletSpeedMax);
                _shotAttackPowerText.text = _shotAttackPower.value.ToString();
                _coolTimeText.text = _coolTime.value.ToString();
                _bulletSpeedText.text = _bulletSpeed.value.ToString();
                break;

            case ParamName.CoolTime:
                _shotAttackPower.SetValueWithoutNotify(attackPowerMax);
                _coolTime.SetValueWithoutNotify(value);
                _bulletSpeed.SetValueWithoutNotify(bulletSpeedMax);
                _shotAttackPowerText.text = _shotAttackPower.value.ToString();
                _coolTimeText.text = _coolTime.value.ToString();
                _bulletSpeedText.text = _bulletSpeed.value.ToString();
                break;

            case ParamName.BulletSpeed:
                _shotAttackPower.SetValueWithoutNotify(attackPowerMax);
                _coolTime.SetValueWithoutNotify(coolTimeMax);
                _bulletSpeed.SetValueWithoutNotify(value);
                _shotAttackPowerText.text = _shotAttackPower.value.ToString();
                _coolTimeText.text = _coolTime.value.ToString();
                _bulletSpeedText.text = _bulletSpeed.value.ToString();
                break;
        }

    }
}
