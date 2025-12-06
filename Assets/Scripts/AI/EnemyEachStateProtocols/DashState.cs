using UnityEngine;

public class DashState : IEnemyState
{

    private TestAIController _controller;
    private Rigidbody _onBallRigidBody;
    private Rigidbody _ballRigidBody;

    private EnemyContext _ctx;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _controller = controller;
        _ctx = ctx;
        Vector3 targetPos = _controller.CalcTargetPos();
        Vector3 direction = (targetPos - _ctx.Transform.position).normalized;
        direction.y = 0;
        _ballRigidBody.AddForce(direction * _ctx.DashPower, ForceMode.Impulse);
        Exit();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        _controller.ThinkNextMove();
    }

}
