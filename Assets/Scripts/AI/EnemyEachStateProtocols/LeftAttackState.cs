using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftAttackState : IEnemyState
{
    private Vector3 _targetDirection = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private bool _canRush = false;
    private bool _isTouchTheEnemy = false;
    private TestAIController _controller;
    private EnemyDetectGround _ground;
    private EnemyContext _ctx;
    private PlayAnimationScript _anim;
    private float _curRushTime = 0;
    private float _currentAtosuki = 0;
    private bool _canAtosukiCount = false;

    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _currentAtosuki = 0;
        _canRush = true;
        _ctx = ctx;
        _controller = controller;
        _anim = _ctx.Animation;
        _anim.LeftATKRush();
        Vector3 target = _ctx.PlayerPosition;
        _targetDirection = (target- _ctx.OnBallRigidbody.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(_targetDirection, Vector3.up);
        _ctx.OnBallRigidbody.rotation = targetRot;
        _targetPos = FinalDestination(_targetDirection, _ctx.RushSpeed);
        _ctx.SoundPlayScript.PlayMeleeSE();
    }

    public void FixedUpdate()
    {
        Vector3 target = _ctx.PlayerPosition;
        _targetDirection = (target - _ctx.OnBallRigidbody.position).normalized;
        Vector3 lookDir = _targetDirection;
        lookDir.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
        _ctx.OnBallRigidbody.rotation = targetRot;
        if (!_canRush)
        {
            return;
        }
        //OnDrawGizmos();
        RaycastHit hit;
        Vector3 attackRangeCenter = _ctx.OnBallRigidbody.transform.position;
        attackRangeCenter.y += _ctx.MeleeRangeCenter.y;
        attackRangeCenter.z += _ctx.MeleeRangeCenter.z;
        if (Physics.BoxCast(attackRangeCenter,
        _ctx.MeleeRangeSize * 0.5f,
        _targetDirection,                
        out hit,
        Quaternion.identity,
        2f))                            
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("Player"))
            {
                _isTouchTheEnemy = true;
                _anim.LeftATKProtocol();
                EnemyToDamageProtocol(hitObj);
                _canAtosukiCount = true;
            }
        }
        _ctx.OnBallRigidbody.MovePosition(Vector3.MoveTowards(_ctx.OnBallRigidbody.position, _targetPos, _ctx.RushSpeed * Time.fixedDeltaTime));
        _curRushTime += Time.fixedDeltaTime;
        if (_curRushTime >= _ctx.RushTime)
        {
            _canRush = false;
            if (!_isTouchTheEnemy)
            {
                _anim.LeftATKProtocol();
                _canAtosukiCount = true;
            }
            _isTouchTheEnemy = false;
            
        }

        if (!_canAtosukiCount)
        {
            return;
        }
        _currentAtosuki += Time.deltaTime;
        if(_currentAtosuki >= _ctx.Atosuki)
        {
            _controller.ThinkNextMove();
        }
    }
    private void EnemyToDamageProtocol(GameObject enemy)
    {
        TakeDamageScript enDamage = enemy.GetComponent<TakeDamageScript>();
        if (enDamage == null)
        {
            return;
        }

        enDamage.MeleeTakeDamage(_targetDirection, _ctx.MeleeDamage,_ctx.MeleeBlowAwayPower);
    }

    private Vector3 FinalDestination(Vector3 direction, float moveSpeed)
    {
        Vector3 finalDestination = _ctx.OnBallRigidbody.position + direction * _ctx.RushSpeed * _ctx.RushTime;
        return finalDestination;
    }
}
