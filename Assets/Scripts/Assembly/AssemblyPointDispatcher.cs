using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public enum ParamName
{
    MeleeAttackPower,
    MeleeRushSpeed,
    MeleeBlowAway,
    ShotWeaponAttackPower,
    CoolTime,
    BulletSpeed,
}

public class AssemblyPointDispatcher : MonoBehaviour
{
    [SerializeField,Header("振り分けに使用できるポイント")]
    private int _canUsePoint = 180;

    private AssemblyViewer _viewer = default;

    private int _canMeleeUsePoint = 0;
    private int _canShotUsePoint = 0;

    private int _meleeAttackPower = 0;
    private int _meleeRushSpeed = 0;
    private int _meleeBlowAway = 0;

    private int _shotWeaponAttackPower = 0;
    private int _coolTime = 0;
    private int _bulletSpeed = 0;

    private void Start()
    {
        _canMeleeUsePoint = _canUsePoint;
        _canShotUsePoint = _canUsePoint;
        _viewer = GetComponent<AssemblyViewer>();
    }

    public void ValueChange(float value,ParamName name,bool isMelee)
    {
        Debug.Log("値変更が呼び出された");
        int point = (int)value;
        int used = 0;
        int over = 0;
        int take = 0;

        if (isMelee)
        {
            int meleeRemain = _canUsePoint - (_meleeAttackPower + _meleeRushSpeed + _meleeBlowAway);
            switch (name)
            {
                case ParamName.MeleeAttackPower:
                    _meleeAttackPower = point;
                    used = _meleeAttackPower + _meleeRushSpeed + _meleeBlowAway;
                    over = used - _canUsePoint;
                    if(over > 0)
                    {
                        if (IsFirstArgmentBiggest(_meleeRushSpeed, _meleeBlowAway))
                        {
                            take = Mathf.Min(over, _meleeRushSpeed);
                            _meleeRushSpeed -= take;
                            over -= take;
                            if(over > 0)
                            {
                                take = Mathf.Min(over, _meleeBlowAway);
                                _meleeBlowAway -= take;
                                over -= take;
                            }
                        }
                        else
                        {
                            take = Mathf.Min(over, _meleeBlowAway);
                            _meleeBlowAway -= take;
                            over -= take;
                            if(over > 0)
                            {
                                take = Mathf.Min(over, _meleeRushSpeed);
                                _meleeRushSpeed -= take;
                                over -= take;
                            }
                        }
                    }
                    meleeRemain = _canUsePoint - (_meleeAttackPower + _meleeRushSpeed + _meleeBlowAway);
                    _viewer.MeleeUpdateValue(_meleeAttackPower, meleeRemain, _meleeAttackPower, _meleeRushSpeed,_meleeBlowAway, ParamName.MeleeAttackPower);
                    break;

                case ParamName.MeleeRushSpeed:
                    _meleeRushSpeed = point;
                    used = _meleeAttackPower + _meleeRushSpeed + _meleeBlowAway;
                    over = used - _canUsePoint;
                    if (over > 0)
                    {
                        if (IsFirstArgmentBiggest(_meleeAttackPower, _meleeBlowAway))
                        {
                            take = Mathf.Min(over, _meleeAttackPower);
                            _meleeAttackPower -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _meleeBlowAway);
                                _meleeBlowAway -= take;
                                over -= take;
                            }
                        }
                        else
                        {
                            take = Mathf.Min(over, _meleeBlowAway);
                            _meleeBlowAway -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _meleeAttackPower);
                                _meleeAttackPower -= take;
                                over -= take;
                            }
                        }
                    }
                    meleeRemain = _canUsePoint - (_meleeAttackPower + _meleeRushSpeed + _meleeBlowAway);
                    _viewer.MeleeUpdateValue(_meleeRushSpeed, meleeRemain, _meleeAttackPower, _meleeRushSpeed, _meleeBlowAway, ParamName.MeleeRushSpeed);
                    break;

                case ParamName.MeleeBlowAway:
                    _meleeBlowAway = point;
                    used = _meleeAttackPower + _meleeRushSpeed + _meleeBlowAway;
                    over = used - _canUsePoint;
                    if (over > 0)
                    {
                        if (IsFirstArgmentBiggest(_meleeAttackPower, _meleeRushSpeed))
                        {
                            take = Mathf.Min(over, _meleeAttackPower);
                            _meleeAttackPower -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _meleeRushSpeed);
                                _meleeRushSpeed -= take;
                                over -= take;
                            }
                        }
                        else
                        {
                            take = Mathf.Min(over, _meleeRushSpeed);
                            _meleeRushSpeed -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _meleeAttackPower);
                                _meleeAttackPower -= take;
                                over -= take;
                            }
                        }
                    }
                    meleeRemain = _canUsePoint - (_meleeAttackPower + _meleeRushSpeed + _meleeBlowAway);
                    _viewer.MeleeUpdateValue(_meleeBlowAway,meleeRemain, _meleeAttackPower, _meleeRushSpeed, _meleeBlowAway, ParamName.MeleeBlowAway);
                    break;

            }
        }
        else
        {
            int shotRemain = _canUsePoint - _shotWeaponAttackPower + _coolTime + _bulletSpeed;
            _canShotUsePoint = _canUsePoint- _shotWeaponAttackPower + _coolTime + _bulletSpeed;
            switch (name)
            {
                case ParamName.ShotWeaponAttackPower:
                    _shotWeaponAttackPower = point;
                    used = _shotWeaponAttackPower + _coolTime + _bulletSpeed;
                    over = used - _canUsePoint;
                    if (over > 0)
                    {
                        if (IsFirstArgmentBiggest(_coolTime, _bulletSpeed))
                        {
                            take = Mathf.Min(over, _coolTime);
                            _coolTime-= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _bulletSpeed);
                                _bulletSpeed -= take;
                                over -= take;
                            }
                        }
                        else
                        {
                            take = Mathf.Min(over, _bulletSpeed);
                            _bulletSpeed -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _coolTime);
                                _coolTime -= take;
                                over -= take;
                            }
                        }
                    }
                    shotRemain = _canUsePoint - (_shotWeaponAttackPower + _coolTime + _bulletSpeed);

                    _viewer.ShotUpdateValue(_shotWeaponAttackPower, shotRemain,_shotWeaponAttackPower,_coolTime,_bulletSpeed, ParamName.ShotWeaponAttackPower);
                    break;

                case ParamName.CoolTime:
                    _coolTime = point;
                    used = _shotWeaponAttackPower + _coolTime + _bulletSpeed;
                    over = used - _canUsePoint;
                    if (over > 0)
                    {
                        if (IsFirstArgmentBiggest(_shotWeaponAttackPower, _bulletSpeed))
                        {
                            take = Mathf.Min(over, _shotWeaponAttackPower);
                            _shotWeaponAttackPower -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _bulletSpeed);
                                _bulletSpeed -= take;
                                over -= take;
                            }
                        }
                        else
                        {
                            take = Mathf.Min(over, _bulletSpeed);
                            _bulletSpeed -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _shotWeaponAttackPower);
                                _shotWeaponAttackPower -= take;
                                over -= take;
                            }
                        }
                    }
                    shotRemain = _canUsePoint - (_shotWeaponAttackPower + _coolTime + _bulletSpeed);

                    _viewer.ShotUpdateValue(_coolTime, shotRemain, _shotWeaponAttackPower, _coolTime, _bulletSpeed, ParamName.CoolTime);

                    break;

                case ParamName.BulletSpeed:
                    _bulletSpeed = point;
                    used = _shotWeaponAttackPower + _coolTime + _bulletSpeed;
                    over = used - _canUsePoint;
                    if (over > 0)
                    {
                        if (IsFirstArgmentBiggest(_shotWeaponAttackPower, _coolTime))
                        {
                            take = Mathf.Min(over, _shotWeaponAttackPower);
                            _shotWeaponAttackPower -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _coolTime);
                                _coolTime -= take;
                                over -= take;
                            }
                        }
                        else
                        {
                            take = Mathf.Min(over, _coolTime);
                            _coolTime -= take;
                            over -= take;
                            if (over > 0)
                            {
                                take = Mathf.Min(over, _shotWeaponAttackPower);
                                _shotWeaponAttackPower -= take;
                                over -= take;
                            }
                        }
                    }
                    shotRemain = _canUsePoint - (_shotWeaponAttackPower + _coolTime + _bulletSpeed);
                    _viewer.ShotUpdateValue(_bulletSpeed,shotRemain, _shotWeaponAttackPower, _coolTime, _bulletSpeed, ParamName.BulletSpeed);
                    break;
            }
        }
    }

    private bool IsFirstArgmentBiggest(int one,int two)
    { 
        if(one > two)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
