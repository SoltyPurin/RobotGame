using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField, Header("近接攻撃のスクリプト")]
    private LeftAttack _meleeAttack = default;
    [SerializeField, Header("射撃のクールタイム")]
    private float _shootCoolTime = 1.0f;

    private InputAction _dashButton = default;
    private InputAction _jumpButton = default;
    private InputAction _lockOnButton = default;
    private InputAction _moveInput = default;
    private InputAction _rightWeaponInput = default;
    private InputAction _leftWeaponInput = default;
    private Jump _jump = default;
    private LockOn _lockOn = default;
    private Dash _dash = default;
    private PlayerMove _move = default;
    private AttackScript _attack = default;
    private PlayAnimationScript _anim = default;
    private TakeDamageScript _takeDamage = default;

    private bool _isAlive = true;

    private float _prevInputTime = 0f;
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
        _dash = GetComponent<Dash>();
        _move = GetComponent<PlayerMove>();
        _attack = GetComponent<AttackScript>();
        _anim = GetComponent<PlayAnimationScript>();
        _takeDamage = GetComponent<TakeDamageScript>();
        _prevInputTime = Time.time;
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
            _dash.DashProtocol(_move.UseVelocity);
        }
        if (_rightWeaponInput.WasPressedThisFrame())
        {
            float curInputTime = Time.time;
            CallRightAttackProtocol(_prevInputTime,curInputTime);
        }
        if (_leftWeaponInput.WasPressedThisFrame())
        {
            _attack.LeftAttack(_lockOn.CurrentTargetObject());
            _anim.LeftATKRush();
        }
    }

    public void Dead()
    {
        _isAlive = false;
    }

    private void CallRightAttackProtocol(float prevInputTime,float currentInputTime)
    {
        float timeDifference = currentInputTime - prevInputTime;
        if (timeDifference > _shootCoolTime)
        {
            _attack.RightAttack(_lockOn.CurrentTargetObject());
            _anim.RightAttackAnim();
            _prevInputTime = currentInputTime;
        }
        else
        {
            float waitTime = 1 - timeDifference;
            Debug.Log("あと" + waitTime + "秒必要");
        }
    }
}
