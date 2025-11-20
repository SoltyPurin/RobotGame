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
    [SerializeField, Header("旋回速度")]
    private float _turningSpeed = 20;

    private RaycastHit _hit;
    private float _verticalValue = 0.0f;
    private float _horizontalValue = 0.0f;
    private float _sphereRadius = 0;
    private Vector2 _moveValue = Vector2.zero;

    private InputAction _moveInput;

    private void Start()
    {
        _sphereRadius = _ballRigidBody.gameObject.GetComponent<SphereCollider>().radius + 0.2f;
        _moveInput = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        _moveValue = _moveInput.ReadValue<Vector2>();
        _verticalValue = _moveValue.y;
        _horizontalValue = _moveValue.x;
        Physics.Raycast(_ballRigidBody.position, Vector3.down, out _hit, _sphereRadius);

    }

    private void FixedUpdate()
    {
        Vector3 curSpeed = Vector3.Lerp(_ballRigidBody.linearVelocity, this.gameObject.transform.forward * _verticalValue * _speed, _accelarationValue);
        _ballRigidBody.linearVelocity = curSpeed;
        _onBallRigidBody.AddTorque(Vector3.up * (_horizontalValue * _turningSpeed));

        _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);

        //Lerpは現状の回転量と目的の回転量で、現状はRigidBody.rotationで出る
        //第一は上物のrotation、第二は↓のやつ
        //Quaternion rota = Quaternion.Slerp(_onBallRigidBody.transform.rotation, Quaternion.FromToRotation(_onBallRigidBody.transform.up, _hit.normal) * _onBallRigidBody.transform.rotation, 1);
        //_onBallRigidBody.MoveRotation(rota);

        Vector3 inputDir = new Vector3(_moveValue.x, 0f, _moveValue.y).normalized;
        if(inputDir.x <= 0.1f && inputDir.z <= 0.1f)
        {
            Debug.Log("入力ナシ");
            return;
        }
        Quaternion.LookRotation(inputDir);
        float angle = Mathf.Atan2(inputDir.z, inputDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.up);

        _onBallRigidBody.rotation = Quaternion.Slerp(_onBallRigidBody.rotation, targetRotation, 0.02f);

    }

}
