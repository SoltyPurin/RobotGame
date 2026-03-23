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
    private Jump _jump = default;
    private LockOn _lockOn = default;
    private PlayerMove _move = default;
    private AttackScript _attack = default;
    private PlayAnimationScript _anim = default;
    private TakeDamageScript _takeDamage = default;
    private AuraBurst _burst = default;
    private PauseManager _pause = default;
    private MoveGage _moveGage = default;

    private bool _isAlive = true;
    private bool _isShootCoolTime = false;
    private bool _isMeleeCoolTime = false;
    private bool _isJumpPressing = false;
    private bool _isBurstPermancing = false;

    private float _prevMeleeInputTime = 0;

    private float _getCoolTimeDivisionValue = 0.01f;

    private void OnEnable()
    {
        _actionMap = new InputSystem_Actions();
        _actionMap.Player.AuraBurst.performed += AuraBurstProtocol;
        _actionMap.Player.Dash.performed += DashProtocol;
        _actionMap.Player.Jump.started += JumpOrDash;
        _actionMap.Player.Jump.canceled += JumpSwitchFalse;
        _actionMap.Player.RightAttack.performed += RightAttackProtocol;
        _actionMap.Player.LeftAttack.performed += LeftAttackProtocol;
        _actionMap.Player.Move.started += MoveProtocol;
        _actionMap.Player.Move.performed += MoveProtocol;
        _actionMap.Player.Move.canceled += MoveProtocol;
        _actionMap.Player.LockOn.performed += LockOnProtocol;
        _actionMap.Player.Pause.performed += PauseProtocol;
        _actionMap.Enable();
    }

    private void Awake()
    {
        float shotCoolTimeMinusValue = PlayerPrefs.GetInt(AssemblyPointDispatcher.CoolTime) * _getCoolTimeDivisionValue;
        _shootCoolTime.Value = Mathf.Clamp(_shootCoolTime.Value, 0.1f, _shootCoolTime.Value - shotCoolTimeMinusValue);
        _saveShootCoolTime = _shootCoolTime.Value;


        _burst = GetComponent<AuraBurst>();
        _jump = GetComponent<Jump>();
        _lockOn = GetComponent<LockOn>();
        _move = GetComponent<PlayerMove>();
        _attack = GetComponent<AttackScript>();
        _anim = GetComponent<PlayAnimationScript>();
        _takeDamage = GetComponent<TakeDamageScript>();
        _moveGage = GetComponent<MoveGage>();
        _pause = FindAnyObjectByType<PauseManager>();
        _prevMeleeInputTime = Time.time;
        _currentMeleeCoolTime = _meleeCoolTime;
        _move.Initialize(_anim,_lockOn,_moveGage);
        _lockOn.Initialize();
    }

    private void FixedUpdate()
    {
        if (IsDontMove())
        {
            return;
        }
        if (_isJumpPressing && _moveGage.MoveTimeProperty.Value >0)
        {
            _moveGage.Moveing(true);
            _jump.JumpProtocol();
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

    private void LockOnProtocol(InputAction.CallbackContext context)
    {
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
    private void JumpOrDash(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return;
        }
        _isJumpPressing = true;

        _anim.JumpingAnim();
    }

    private void JumpSwitchFalse(InputAction.CallbackContext context)
    {
        _isJumpPressing = false;
        _anim.FallingAnim();
    }
    private void DashProtocol(InputAction.CallbackContext context)
    {
        if (IsDontMove())
        {
            return ;
        }
        Debug.Log("ダッシュ入力");
        _move.DashProtocol();
        _anim.DashSwitch(_move.IsRunning.Value);
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
    private void PauseProtocol(InputAction.CallbackContext context)
    {
        _pause.InputPause();
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
            Debug.Log("撃破");
            _actionMap.Player.AuraBurst.performed -= AuraBurstProtocol;
            _actionMap.Player.Dash.performed -= DashProtocol;
            _actionMap.Player.Jump.started -= JumpOrDash;
        _actionMap.Player.Jump.canceled -= JumpSwitchFalse;
            _actionMap.Player.RightAttack.performed -= RightAttackProtocol;
            _actionMap.Player.LeftAttack.performed -= LeftAttackProtocol;
            _actionMap.Player.Move.started -= MoveProtocol;
            _actionMap.Player.Move.performed -= MoveProtocol;
            _actionMap.Player.Move.canceled -= MoveProtocol;
            _actionMap.Player.Pause.performed -= PauseProtocol;
            _actionMap.Player.LockOn.performed -= LockOnProtocol;

        _isAlive = false;

    }
    private void CallLeftAttackProtocol(float prevInputTime,float curInputTime)
    {
        var enemy = _lockOn.CurrentTargetObject();
        if(enemy == null)
        {
            enemy = _tempTarget;
        }
        if (!_isMeleeCoolTime)
        {
            Debug.Log("近接振ってる");
            _attack.LeftAttack(enemy);
            _anim.LeftATKRush();
            _isMeleeCoolTime=true;
        }
    }

    private void CallRightAttackProtocol(float currentInputTime)
    {
        Transform enemy = _lockOn.CurrentTargetObject();
        if (enemy == null)
        {
            enemy = _tempTarget;
        }
        if (!_isShootCoolTime)
        {
            _isShootCoolTime = true;
            _attack.RightAttack(enemy);
            _anim.RightAttackAnim();
        }
    }
/// <summary>
/// バーストが発動した時と非発動時のフラグを帰る
/// </summary>
/// <param name="isActive">バーストが発動したか</param>
public void InBurstMove(bool isActive)
{
        if (isActive)
        {
            _isBurstPermancing = true;
        }
        else
        {
            _isBurstPermancing= false;
        }
}

private bool IsDontMove()
    {
        bool canMove = !_isAlive && _takeDamage.IsBlowning && _meleeAttack.IsRushing && !_isBurstPermancing;
        return canMove;
    }

    private void OnDisable()
    {
        Dead();
        _actionMap?.Dispose();
        _actionMap?.Disable();
    }

}
