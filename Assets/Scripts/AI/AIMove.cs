using UnityEngine;

public class AIMove : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("移動速度")]
    private float _aiMoveSpeed = 50f;

    private AIControllScript _controller = default;

    public void MoveProtocol(Vector3 targetPos,float moveDistance)
    {
        Debug.Log("移動中");
        float distance = Vector3.Distance(targetPos,this.transform.position);
        Vector3 moveDirection = (targetPos - this.transform.position).normalized;
        Vector3 curVelocity = _ballRigidBody.linearVelocity;
        if (distance > moveDistance)
        {
            Quaternion targetRot = Quaternion.LookRotation(transform.forward, Vector3.up);
            Quaternion temp = Quaternion.RotateTowards(_onBallRigidBody.rotation, targetRot, 600 * Time.fixedDeltaTime);
            _onBallRigidBody.rotation = temp;
            Vector3 useVelocity = moveDirection * _aiMoveSpeed;
            useVelocity.y = curVelocity.y;
            _ballRigidBody.linearVelocity = useVelocity;

        }
        else
        {
            _controller.ThinkNextMove();
        }
    }
}
