using UnityEngine;

public class DashState : IEnemyState
{

    private TestAIController _controller;
    private Rigidbody _onBallRigidBody;
    private Rigidbody _ballRigidBody;

    private EnemyContext _ctx;

    private float _curDodgeTime = 0;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _controller = controller;
        _ctx = ctx;
        Vector3 targetPos = _controller.CalcTargetPos();
        Vector3 direction = (targetPos - _ctx.Transform.position).normalized;
        direction.y = 0;
        _ctx.BallRigidBody.AddForce(direction * _ctx.DodgePower, ForceMode.Impulse);
    }

    public void FixedUpdate()
    {
        _curDodgeTime += Time.deltaTime;
        if(_curDodgeTime >= _ctx.DodgeTime)
        {
            _controller.ThinkNextMove();
        }
    }

}
