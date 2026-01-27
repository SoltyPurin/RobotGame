using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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
    [SerializeField, Header("ターゲットが無い時の仮ターゲット")]
    private Transform _tempTarget = default;
    private float _currentMeleeCoolTime = default;

    private InputSystem_Actions _actionMap = default;
    private InputAction _lockOnButton = default;
    private Jump _jump = default;
    private LockOn _lockOn = default;
    private PlayerMove _move = default;
    private AttackScript _attack = default;
    private PlayAnimationScript _anim = default;
    private TakeDamageScript _takeDamage = default;
    private AuraBurst _burst = default;

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
        _actionMap = new InputSystem_Actions();
        _actionMap.Player.AuraBurst.performed += AuraBurstProtocol;
        _actionMap.Player.Dash.performed += DashProtocol;
        _actionMap.Player.Jump.performed += JumpProtocol;
        _actionMap.Player.RightAttack.performed += RightAttackProtocol;
        _actionMap.Player.LeftAttack.performed += LeftAttackProtocol;
        _actionMap.Player.Move.started += MoveProtocol;
        _actionMap.Player.Move.performed += MoveProtocol;
        _actionMap.Player.Move.canceled += MoveProtocol;
        _actionMap.Player.LockOn.performed += LockOnProtocol;

        _actionMap.Enable();
        _burst = GetComponent<AuraBurst>();
        _lockOnButton = InputSystem.actions.FindAction("LockOn");
        _jump = GetComponent<Jump>();
        _lockOn = GetComponent<LockOn>();
        _move = GetComponent<PlayerMove>();
        _attack = GetComponent<AttackScript>();
        _anim = GetComponent<PlayAnimationScript>();
        _takeDamage = GetComponent<TakeDamageScript>();
        _prevMeleeInputTime = Time.time;
        _saveShootCoolTime = _shootCoolTime.Value;
        _currentMeleeCoolTime = _meleeCoolTime;
        _lockOn.Initialize();
    }

    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }
        //if (_lockOnButton.WasPressedThisFrame())
        //{
        //    _lockOn.ChangeCamera();
        //}

        if (_takeDamage.IsBlowning)
        {
            return;
        }
        if (_meleeAttack.IsRushing)
        {
            return;
        }
        //Vector2 input = _moveInput.ReadValue<Vector2>();
        //_move.InputProtocol(input); 
        //if(input.magnitude <= 0)
        //{
        //    _anim.IdleAnim();
        //}
        //else
        //{
        //    _anim.MoveAnim();
        //}
        //if (_jumpButton.WasPressedThisFrame())
        //{
        //    _jump.JumpProtocol();
        //    _anim.JumpAnim();
        //}
        //if(_dashButton.WasPressedThisFrame())
        //{
        //    _move.DashProtocol();
        //}
        //if (_rightWeaponInput.WasPressedThisFrame())
        //{
        //    float curInputTime = Time.time;
        //    CallRightAttackProtocol(curInputTime);
        //}
        //if (_leftWeaponInput.WasPressedThisFrame())
        //{
        //    float curInputTime = Time.time;
        //    CallLeftAttackProtocol(_prevMeleeInputTime,curInputTime);
        //}

        if (_isShootCoolTime)
        {
            ShootCoolTimeCounter();
        }

        if(_isMeleeCoolTime)
        {
            MeleeCoolTimeCounter();
        }

    }

    private void LockOnProtocol(InputAction.CallbackContext context)
    {
        if (!_isAlive)
        {
            return;
        }
        _lockOn.ChangeCamera();


    }

    private void MoveProtocol(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return;
        }
        Vector2 input = context.ReadValue<Vector2>();
        _move.InputProtocol(input);
        if (input.magnitude <= 0)
        {
            _anim.IdleAnim();
        }
        else
        {
            _anim.MoveAnim();
        }

    }
    private void JumpProtocol(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return;
        }
        _jump.JumpProtocol();
        _anim.JumpAnim();

    }
    private void DashProtocol(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return ;
        }

        _move.DashProtocol();
    }
    private void RightAttackProtocol(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return;
        }

        float curInputTime = Time.time;
        CallRightAttackProtocol(curInputTime);
    }

    private void LeftAttackProtocol(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return;
        }

        float curInputTime = Time.time;
        CallLeftAttackProtocol(_prevMeleeInputTime, curInputTime);
    }
    private void AuraBurstProtocol(InputAction.CallbackContext context)
    {
        _burst.StartAuraBurst();
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
        Transform enemy = _lockOn.CurrentTargetObject();
        //if(enemy == null)
        //{
        //    enemy = _tempTarget;
        //} 
        if (!_isShootCoolTime)
        {
            _isShootCoolTime = true;
            _attack.RightAttack(enemy);
            _anim.RightAttackAnim();
        }
    }

    private bool IsDontMove()
    {
        bool canMove = !_isAlive && _takeDamage.IsBlowning && _meleeAttack.IsRushing;
        return canMove;
    }

    private void OnDestroy()
    {
        _actionMap.Player.AuraBurst.performed-= AuraBurstProtocol;
        _actionMap.Player.Dash.performed -= DashProtocol;
        _actionMap.Player.Jump.performed -= JumpProtocol;
        _actionMap.Player.RightAttack.performed -= RightAttackProtocol;
        _actionMap.Player.LeftAttack.performed -= LeftAttackProtocol;
        _actionMap.Player.Move.started -= MoveProtocol;
        _actionMap.Player.Move.performed -= MoveProtocol;
        _actionMap.Player.Move.canceled -= MoveProtocol;

        _actionMap.Disable();
        _actionMap?.Dispose();
    }

}
