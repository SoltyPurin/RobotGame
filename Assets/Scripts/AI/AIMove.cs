using UnityEngine;

public class AIMove : MonoBehaviour/*,IEnemyState*/
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("移動速度")]
    private float _aiMoveSpeed = 50f;
    [SerializeField,Header("コントローラー")]
    private AIControllScript _controller = default;
    [SerializeField, Header("重力")]
    private float _downForce = 150;

    private void FixedUpdate()
    {
        _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);
    }
    public void MoveProtocol(Vector3 targetPos,float moveDistance)
    {
        float distance = Vector3.Distance(targetPos,this.transform.position);
        Vector3 moveDirection = (targetPos - this.transform.position).normalized;
        Vector3 curVelocity = _ballRigidBody.linearVelocity;
        if (distance > moveDistance)
        {
            Debug.Log("移動中");
            Quaternion targetRot = Quaternion.LookRotation(moveDirection, Vector3.up);
            Quaternion temp = Quaternion.RotateTowards(_onBallRigidBody.rotation, targetRot, 600 * Time.fixedDeltaTime);
            _onBallRigidBody.rotation = temp;
            Vector3 useVelocity = moveDirection * _aiMoveSpeed;
            useVelocity.y = curVelocity.y;
            _ballRigidBody.linearVelocity = useVelocity;

        }
        else
        {
            Debug.Log("目的地に到着");
            _controller.ThinkNextMove();
        }
    }
}
