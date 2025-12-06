using UnityEngine;

public class MoveState : MonoBehaviour, IEnemyState
{
    private TestAIController _controller;
    private Rigidbody _onBallRigidBody;
    private Rigidbody _ballRigidBody;

    private EnemyContext _ctx;
    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        Debug.Log("コントローラー設定");
        _controller = controller;
        _ctx = ctx;
    }

    public void Update()
    {
        if(_controller == null)
        {
            Debug.Log("コントローラーない");
            return;
        }
        Debug.Log("移動中");

        Vector3 targetPos = _controller.CalcTargetPos();
        float distance = Vector3.Distance(targetPos, this.transform.position);
        Vector3 moveDirection = (targetPos - this.transform.position).normalized;
        Vector3 curVelocity = _ballRigidBody.linearVelocity;
        if (distance > _controller.NearTargetPosDistance)
        {
            Debug.Log("移動中");
            Quaternion targetRot = Quaternion.LookRotation(moveDirection, Vector3.up);
            Quaternion temp = Quaternion.RotateTowards(_onBallRigidBody.rotation, targetRot, 600 * Time.fixedDeltaTime);
            _onBallRigidBody.rotation = temp;
            Vector3 useVelocity = moveDirection * _controller.AIMoveSpeed;
            useVelocity.y = curVelocity.y;
            _ballRigidBody.linearVelocity = useVelocity;

        }
        else
        {
            Debug.Log("目的地に到着");
            Exit();
        }
}

public void Exit()
    {

    }

}
