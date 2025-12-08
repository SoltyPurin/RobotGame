using Cysharp.Threading.Tasks;
using UnityEngine;

public class LeftAttackState : MonoBehaviour,IEnemyState
{
    private Vector3 _targetDirection = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private bool _canRush = false;
    private bool _isTouchTheEnemy = false;
    private TestAIController _controller;
    private EnemyDetectGround _ground;
    private EnemyContext _ctx;
    private PlayAnimationScript _anim;


    public void Enter(in TestAIController controller, in EnemyContext ctx)
    {
        _canRush = true;
        _ctx = ctx;
        _controller = controller;
        _anim = _ctx.Animation;
        _anim.LeftATKRush();
        Vector3 target = _ctx.PlayerTransform.position;
        _targetDirection = (target- _ctx.OnBallRigidbody.position).normalized;
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
        if (Physics.BoxCast(_ctx.OnBallRigidbody.transform.position, (Vector3.one * 0.5f), new Vector3(0, -1, 0), out hit, Quaternion.identity, 1))
        {
            Debug.Log("“–‚½‚Á‚½");
            //hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.7f, 0.7f, 0.1f);
        }
        _ctx.OnBallRigidbody.MovePosition(Vector3.MoveTowards(_ctx.OnBallRigidbody.position, _targetPos, _ctx.RushSpeed * Time.fixedDeltaTime));

        Debug.Log("ƒ_ƒ“ƒoƒCƒ“");
    }

    private async void StopRush()
    {
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -1, 0), Vector3.one);
    }
}
