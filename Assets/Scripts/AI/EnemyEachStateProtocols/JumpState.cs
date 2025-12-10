using UnityEngine;

public class JumpState : Jump, IEnemyState
{
    private TestAIController _controller;
    private EnemyDetectGround _ground;

    private EnemyContext _ctx;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        Debug.Log("ジャンプ呼び出し");
        _controller = controller;
        _ctx = ctx; 
        _ground = _ctx.Ground;
        _ctx.Animation.JumpAnim();
        if (!_ground.IsTouchTheGround)
        {
            return;
        }
        _ctx.BallRigidBody.AddForce(_ctx.Transform.up * _ctx.JumpPower, ForceMode.Impulse);
    }

    public void Update()
    {
        Debug.Log("JumpState.Update 動いてる");
        Debug.Log("接地判定 = " + _ground.IsTouchTheGround);
        if (_ground.IsTouchTheGround)
        {
            _controller.ThinkNextMove();
            Exit();
        }
    }

    public void Exit()
    {
    }

}
