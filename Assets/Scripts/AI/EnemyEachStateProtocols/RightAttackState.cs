using UnityEngine;
using UnityEngine.EventSystems;

public class RightAttackState : IEnemyState
{
    private EnemyContext _ctx;
    private TestAIController _controller;
    private bool _calledNext = false;
    private float _timer = 0f;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _ctx = ctx;
        _controller = controller;
        _ctx.Animation.RightAttackAnim();
        float aliveTime = _ctx.BulletAliveTime;
        int damage = (int)_ctx.BulletDamage;
        float power = _ctx.BulletBlowAwayPower;
        Vector3 targetDIr = (_ctx.PlayerTransform.position - _ctx.ShootPoint.position).normalized;
        _ctx.Pool.ActiveBullet(targetDIr, aliveTime, _ctx.ShootPoint.position, damage, power);
        //controller.ThinkNextMove();
    }

    public void Update()
    {
        _ctx.OnBallRigidbody.transform.LookAt(_ctx.PlayerTransform.position);
        if (_calledNext)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= _ctx.StopTime)
        {
            _calledNext = true;
            _controller.ThinkNextMove();  
        }
    }

    public void Exit()
    {

    }

}
