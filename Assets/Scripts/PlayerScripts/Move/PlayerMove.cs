using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("玉のリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("どれくらい加速しやすいか")]
    private float _accelarationValue = 0.5f;
    [SerializeField, Header("速度")]
    private float _speed = 50;
    [SerializeField, Header("重力")]
    private float _downForce = 5;
    [SerializeField, Header("通常カメラ")]
    private GameObject _normalCamera = default;
    [SerializeField,Header("ロックオンカメラ")]
    private GameObject _lockOnCamera = default;

    private RaycastHit _hit;
    private float _verticalValue = 0.0f;
    private float _horizontalValue = 0.0f;
    private float _sphereRadius = 0;
    private Vector2 _v2MoveValue = Vector2.zero;
    private Quaternion _targeRotation = default;
    private Vector3 _useVelocity = Vector3.zero;
    public Vector3 UseVelocity
    {
        get { return _useVelocity; }
    }

    private InputAction _moveInput;
    private LockOn _lockOn = default;

    private void Start()
    {
        _sphereRadius = _ballRigidBody.gameObject.GetComponent<SphereCollider>().radius + 0.2f;
        _moveInput = InputSystem.actions.FindAction("Move");
        _targeRotation = transform.rotation;
        _lockOn = GetComponent<LockOn>();
    }
    private void Update()
    {
        Physics.Raycast(_ballRigidBody.position, Vector3.down, out _hit, _sphereRadius);

    }
    public void InputProtocol(Vector2 input)
    {
        _v2MoveValue = input;
        _verticalValue = _v2MoveValue.y;
        _horizontalValue = _v2MoveValue.x;
    }

    private void FixedUpdate()
    {
        MoveProtocol();
    }

    private void MoveProtocol()
    {
        GameObject activeCamera = _normalCamera;
        Vector3 curVelocity = _ballRigidBody.linearVelocity;
        if (_lockOn.State == CameraState.LockOn)
        {
            activeCamera = _lockOnCamera;
        }
        Vector3 cameraForward = Vector3.Scale(activeCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * _verticalValue + activeCamera.transform.right * _horizontalValue;
        //Vector3 v3Input = new Vector3(_horizontalValue, 0, _verticalValue);
        if (_v2MoveValue.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveForward, Vector3.up);
            Quaternion temp = Quaternion.RotateTowards(_onBallRigidBody.rotation, targetRot, 600 * Time.fixedDeltaTime);
            _onBallRigidBody.rotation = temp;
            _useVelocity = moveForward * _speed;
            _useVelocity.y = curVelocity.y;
            _ballRigidBody.linearVelocity = _useVelocity;
        }
        else
        {
            _ballRigidBody.linearVelocity *= 0.9f;
        }

        _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);
    }

}
