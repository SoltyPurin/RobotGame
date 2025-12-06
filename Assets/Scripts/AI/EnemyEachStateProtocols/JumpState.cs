using UnityEngine;

public class JumpState : Jump, IEnemyState
{
    private TestAIController _controller;
    private Rigidbody _onBallRigidBody;
    private Rigidbody _ballRigidBody;

    private EnemyContext _ctx;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _controller = controller;
        _ctx = ctx;
        if (_jumpCount >= _canJumpCount)
        {
            return;
        }

        _jumpCount++;
        _ctx.BallRigidBody.AddForce(_ctx.Transform.up * _jumpForce, ForceMode.Impulse);
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
