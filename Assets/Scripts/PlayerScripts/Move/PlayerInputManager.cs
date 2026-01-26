using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のスクリプト")]
    private LeftAttack _meleeAttack = default;
    [SerializeField, Header("射撃のクールタイム")]
    private FloatReactiveProperty _shootCoolTime ;
    private float _saveShootCoolTime = default;
    public IReadOnlyReactiveProperty<float> ShootCoolTimeProperty
    {
        get { return _shootCoolTime; }
    }

    [SerializeField, Header("近接の後隙")]
    private float _meleeCoolTime = 1.0f;
    private float _currentMeleeCoolTime = default;


    private InputAction _dashButton = default;
    private InputAction _jumpButton = default;
    private InputAction _lockOnButton = default;
    private InputAction _moveInput = default;
    private InputAction _rightWeaponInput = default;
    private InputAction _leftWeaponInput = default;
    private Jump _jump = default;
    private LockOn _lockOn = default;
    private DashGage _dash = default;
    private PlayerMove _move = default;
    private AttackScript _attack = default;
    private PlayAnimationScript _anim = default;
    private TakeDamageScript _takeDamage = default;

    private bool _isAlive = true;
    private bool _isShootCoolTime = false;
    private bool _isMeleeCoolTime = false;

    private float _meleeTimeDifference = default;

    private float _prevMeleeInputTime = 0;

    private float _getCoolTimeDivisionValue = 0.01f;

    private void OnEnable()
    {
        float shotCoolTimeMinusValue = PlayerPrefs.GetInt(AssemblyPointDispatcher.CoolTime) * _getCoolTimeDivisionValue + 0.1f;
        _shootCoolTime.Value -= shotCoolTimeMinusValue;
    }
    private void Start()
    {
        _dashButton = InputSystem.actions.FindAction("Dash");
        _lockOnButton = InputSystem.actions.FindAction("LockOn");
        _jumpButton = InputSystem.actions.FindAction("Jump");
        _moveInput = InputSystem.actions.FindAction("Move");
        _rightWeaponInput = InputSystem.actions.FindAction("RightAttack");
        _leftWeaponInput = InputSystem.actions.FindAction("LeftAttack");
        _jump = GetComponent<Jump>();
        _lockOn = GetComponent<LockOn>();
        _dash = GetComponent<DashGage>();
        _move = GetComponent<PlayerMove>();
        _attack = GetComponent<AttackScript>();
        _anim = GetComponent<PlayAnimationScript>();
        _takeDamage = GetComponent<TakeDamageScript>();
        _prevMeleeInputTime = Time.time;
        _saveShootCoolTime = _shootCoolTime.Value;
        _currentMeleeCoolTime = _meleeCoolTime;
    }

    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }
        if (_lockOnButton.WasPressedThisFrame())
        {
            _lockOn.ChangeCamera();
        }

        if (_takeDamage.IsBlowning)
        {
            return;
        }
        if (_meleeAttack.IsRushing)
        {
            return;
        }
        Vector2 input = _moveInput.ReadValue<Vector2>();
        _move.InputProtocol(input); 
        if(input.magnitude <= 0)
        {
            _anim.IdleAnim();
        }
        else
        {
            _anim.MoveAnim();
        }
        if (_jumpButton.WasPressedThisFrame())
        {
            _jump.JumpProtocol();
            _anim.JumpAnim();
        }
        if(_dashButton.WasPressedThisFrame())
        {
            _move.DashProtocol();
        }
        if (_rightWeaponInput.WasPressedThisFrame())
        {
            float curInputTime = Time.time;
            CallRightAttackProtocol(curInputTime);
        }
        if (_leftWeaponInput.WasPressedThisFrame())
        {
            float curInputTime = Time.time;
            CallLeftAttackProtocol(_prevMeleeInputTime,curInputTime);
        }

        if (_isShootCoolTime)
        {
            ShootCoolTimeCounter();
        }

        if(_isMeleeCoolTime)
        {
            MeleeCoolTimeCounter();
        }

    }

    private void ShootCoolTimeCounter()
    {
        _shootCoolTime.Value -= Time.deltaTime;
        if (_shootCoolTime.Value <= 0)
        {
            _shootCoolTime.Value = _saveShootCoolTime;
            _isShootCoolTime = false;
        }
    }

    private void MeleeCoolTimeCounter()
    {
        _currentMeleeCoolTime -= Time.deltaTime;
        if(_currentMeleeCoolTime <= 0)
        {
            _currentMeleeCoolTime = _meleeCoolTime;
            _isMeleeCoolTime = false;
        }
    }

    public void Dead()
    {
        _isAlive = false;
    }
    private void CallLeftAttackProtocol(float prevInputTime,float curInputTime)
    {
        _meleeTimeDifference = curInputTime - prevInputTime;
        if(_meleeTimeDifference > _meleeCoolTime)
        {
            _attack.LeftAttack(_lockOn.CurrentTargetObject());
            _anim.LeftATKRush();
            _isMeleeCoolTime=true;
        }
    }

    private void CallRightAttackProtocol(float currentInputTime)
    {
        //_shootTimeDifference = currentInputTime - prevInputTime;
        if (!_isShootCoolTime)
        {
            _isShootCoolTime = true;
            _attack.RightAttack(_lockOn.CurrentTargetObject());
            _anim.RightAttackAnim();
            //_prevShootInputTime = currentInputTime;
        }
    }
}
