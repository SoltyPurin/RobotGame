using Cysharp.Threading.Tasks;
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
    private bool _stopRushRunning = false;


    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _canRush = true;
        _ctx = ctx;
        _controller = controller;
        _anim = _ctx.Animation;
        _anim.LeftATKRush();
        Vector3 target = _ctx.PlayerTransform.position;
        _targetDirection = (target- _ctx.OnBallRigidbody.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(_targetDirection, Vector3.up);
        _ctx.OnBallRigidbody.rotation = targetRot;
        _targetPos = FinalDestination(_targetDirection, _ctx.RushSpeed);
        StopRush();

    }

    public void Update()
    {
        if (!_canRush)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.BoxCast(
        _ctx.OnBallRigidbody.transform.position,
        Vector3.one * 0.5f,
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
            }
        }
        _ctx.OnBallRigidbody.MovePosition(Vector3.MoveTowards(_ctx.OnBallRigidbody.position, _targetPos, _ctx.RushSpeed * Time.fixedDeltaTime));

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

    private async void StopRush()
    {
        if (_stopRushRunning) return;   
        _stopRushRunning = true;
        await UniTask.WaitForSeconds(_ctx.RushTime);
        _canRush = false;
        if (!_isTouchTheEnemy)
        {
            _anim.LeftATKProtocol();
        }
        _isTouchTheEnemy = false;
        _controller.ThinkNextMove();
    }


    public void Exit()
    {

    }

    private Vector3 FinalDestination(Vector3 direction, float moveSpeed)
    {
        Vector3 finalDestination = _ctx.OnBallRigidbody.position + direction * _ctx.RushSpeed * _ctx.RushTime;
        return finalDestination;
    }
    private void OnDrawGizmos()
    {
        if (_ctx != null && _ctx.OnBallRigidbody != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                _ctx.OnBallRigidbody.position + _targetDirection * 1f,
                Vector3.one
            );
        }
    }
}
