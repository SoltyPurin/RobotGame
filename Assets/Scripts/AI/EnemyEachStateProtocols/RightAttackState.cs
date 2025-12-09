using UnityEngine;

public class RightAttackState : MonoBehaviour, IEnemyState
{
    private EnemyContext _ctx;
    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _ctx = ctx;
        _ctx.Animation.RightAttackAnim();
        float aliveTime = _ctx.BulletAliveTime;
        int damage = (int)_ctx.BulletDamage;
        float power = _ctx.BulletBlowAwayPower;
        Vector3 targetDIr = (_ctx.PlayerTransform.position - _ctx.Transform.position).normalized;
        _ctx.Pool.ActiveBullet(targetDIr, aliveTime, _ctx.Transform.position, damage, power);
        controller.ThinkNextMove();
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

}
