using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private InputAction _dashButton = default;
    private InputAction _jumpButton = default;
    private InputAction _lockOnButton = default;
    private InputAction _moveInput = default;
    private InputAction _rightWeaponInput = default;
    private InputAction _leftWeaponInput = default;
    [SerializeField, Header("ジャンプのスクリプト")]
    private Jump _jump = default;
    [SerializeField, Header("ロックオンのスクリプト")]
    private LockOn _lockOn = default;
    [SerializeField, Header("ダッシュのスクリプト")]
    private Dash _dash = default;
    [SerializeField,Header("移動のスクリプト")]
    private PlayerMove _move = default;
    [SerializeField, Header("攻撃のスクリプト")]
    private AttackScript _attack = default;

    private void Start()
    {
        _dashButton = InputSystem.actions.FindAction("Dash");
        _lockOnButton = InputSystem.actions.FindAction("LockOn");
        _jumpButton = InputSystem.actions.FindAction("Jump");
        _moveInput = InputSystem.actions.FindAction("Move");
        _rightWeaponInput = InputSystem.actions.FindAction("RightAttack");
        _leftWeaponInput = InputSystem.actions.FindAction("LeftAttack");
    }

    private void Update()
    {
        _move.InputProtocol(_moveInput.ReadValue<Vector2>());
        if (_jumpButton.WasPressedThisFrame())
        {
            _jump.JumpProtocol();
        }
        if (_lockOnButton.WasPressedThisFrame())
        {
            _lockOn.ChangeCamera();
        }
        if(_dashButton.WasPressedThisFrame())
        {
            _dash.DashProtocol(_move.UseVelocity);
        }
        if (_rightWeaponInput.WasPressedThisFrame())
        {
            _attack.RightAttack();
        }
        if (_leftWeaponInput.WasPressedThisFrame())
        {
            _attack.LeftAttack();
        }
    }
}
