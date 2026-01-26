using System.Collections;
using UnityEngine;

public class RightAttackState : IEnemyState
{
    private EnemyContext _ctx;
    private TestAIController _controller;
    private bool _calledNext = false;
    private float _timer = 0f;

    private float _shootAidaTime = 0.1f;
    private float _currentShootingTime = 0;

    private Vector3 _targetDirection = Vector3.zero;
    private float _aliveTime = 0;
    private int _damage = 0;
    private float _power = 0;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _ctx = ctx;
        _controller = controller;
        _ctx.Animation.RightAttackAnim();
        _aliveTime = _ctx.BulletAliveTime;
        _damage = (int)_ctx.BulletDamage;
        _power = _ctx.BulletBlowAwayPower;
        _targetDirection = (_ctx.PlayerPosition - _ctx.ShootPoint.position).normalized;
    }


    public void FixedUpdate()
    {
        _ctx.OnBallRigidbody.transform.LookAt(_ctx.PlayerTransform.position);

        _currentShootingTime += Time.fixedDeltaTime;
        if(_currentShootingTime >= _shootAidaTime)
        {
            _ctx.SoundPlayScript.PlayShotSE();
            _currentShootingTime = 0;
            _ctx.Pool.ActiveBullet(_targetDirection, _aliveTime, _ctx.ShootPoint.position, _damage, _power, "ENBullet",_ctx.BulletMoveSpeed);
        }

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
