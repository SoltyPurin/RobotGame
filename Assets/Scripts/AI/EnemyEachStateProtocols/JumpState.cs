using UnityEngine;

public class JumpState : IEnemyState
{
    private TestAIController _controller;
    private EnemyDetectGround _ground;

    private EnemyContext _ctx;

    private float _jumpCurrentTime = 0;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _controller = controller;
        _ctx = ctx; 
        _ground = _ctx.Ground;
        _jumpCurrentTime = 0;
        _ctx.Animation.JumpingAnim(true);
        if (!_ground.IsTouchTheGround)
        {
            return;
        }
        _ctx.BallRigidBody.AddForce(_ctx.Transform.up * _ctx.JumpPower, ForceMode.Impulse);
    }

    public void FixedUpdate()
    {
        if (_ground.IsTouchTheGround)
        {
            _ctx.Animation.JumpingAnim(false);
            _controller.ThinkNextMove();
        }
        else
        {
            _jumpCurrentTime += Time.fixedDeltaTime;
            if (_jumpCurrentTime >= 1)
            {
                _ctx.Animation.JumpingAnim(false);
                _controller.AttackThinkProtocol(_controller.CalcTargetDistance());
                _jumpCurrentTime = 0;
            }
        }
    }


}
