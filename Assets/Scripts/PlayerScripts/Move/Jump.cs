using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField, Header("ƒWƒƒƒ“ƒv—Í")]
    private float _jumpForce = 10f;

    private Rigidbody _onBallRigidBody = default;

    private InputAction _jumpButton;

    private void Start()
    {
        _onBallRigidBody = GetComponent<Rigidbody>();
        _jumpButton = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        
    }
}
