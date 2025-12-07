using UnityEngine;

public class JumpState : Jump, IEnemyState
{
    private TestAIController _controller;
    private Rigidbody _onBallRigidBody;
    private Rigidbody _ballRigidBody;
    private EnemyDetectGround _ground;

    private EnemyContext _ctx;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _controller = controller;
        _ctx = ctx;
        _ground = _ctx.Ground;
        _ctx.BallRigidBody.AddForce(_ctx.Transform.up * _ctx.JumpPower, ForceMode.Impulse);
    }

    public void Update()
    {
        if(_ground.IsTouchTheGround)
        {
            _controller.ThinkNextMove();
        }
    }

    public void Exit()
    {
    }

}
