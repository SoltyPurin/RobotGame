using UnityEngine;

public class MoveState : IEnemyState
{
    private TestAIController _controller;
    private Rigidbody _onBallRigidBody;
    private Rigidbody _ballRigidBody;

    private EnemyContext _ctx;
    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _controller = controller;
        _ctx = ctx;
        _ctx.Animation.MoveAnim();
    }

    public void FixedUpdate()
    {
        _ctx.BallRigidBody.AddForce(-_ctx.Transform.up * _ctx.Gravity * _ctx.BallRigidBody.mass);
        Vector3 targetPos = _controller.CalcTargetPos();
        float distance = Vector3.Distance(targetPos, _ctx.Transform.position);
        Vector3 moveDirection = (targetPos - _ctx.Transform.position).normalized;
        Vector3 lookDir = moveDirection;
        lookDir.y = 0;
        Vector3 curVelocity = _ctx.BallRigidBody.linearVelocity;
        if (distance > _controller.NearTargetPosDistance)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            _ctx.OnBallRigidbody.rotation = targetRot;
            Vector3 useVelocity = moveDirection * _ctx.Controller.AIMoveSpeed;
            useVelocity.y = curVelocity.y;
            _ctx.BallRigidBody.linearVelocity = useVelocity;

        }
        else
        {
            _controller.ThinkNextMove();

        }
    }

public void Exit()
    {
    }

}
