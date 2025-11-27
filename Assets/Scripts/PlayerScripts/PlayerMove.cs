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
    [SerializeField, Header("カメラ")]
    private GameObject _camera = default;

    private RaycastHit _hit;
    private float _verticalValue = 0.0f;
    private float _horizontalValue = 0.0f;
    private float _sphereRadius = 0;
    private Vector2 _v2MoveValue = Vector2.zero;

    private InputAction _moveInput;

    private void Start()
    {
        _sphereRadius = _ballRigidBody.gameObject.GetComponent<SphereCollider>().radius + 0.2f;
        _moveInput = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        _v2MoveValue = _moveInput.ReadValue<Vector2>();
        _verticalValue = _v2MoveValue.y;
        _horizontalValue = _v2MoveValue.x;
        Physics.Raycast(_ballRigidBody.position, Vector3.down, out _hit, _sphereRadius);

    }

    private void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * _verticalValue + _camera.transform.right * _horizontalValue;
        //Vector3 v3Input = new Vector3(_horizontalValue, 0, _verticalValue);
        if(_v2MoveValue.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveForward, Vector3.up);
            _onBallRigidBody.rotation = targetRot;
            _ballRigidBody.linearVelocity = moveForward * _speed;
            _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);
        }
        else
        {
            _ballRigidBody.linearVelocity *=0.9f;
        }


    }

}
