using UnityEngine;
using Cysharp.Threading.Tasks;

public class LeftAttack : MonoBehaviour
{
    [SerializeField, Header("ダッシュ時の速度")]
    private float _dashSpeed = 40f;
    [SerializeField, Header("突進時間")]
    private float _rushTime = 1.4f;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField,Header("アニメーション再生のスクリプト")]
    private PlayerAnimationPlayScript _anim = default;
    [SerializeField,Header("ステートのマネージャー")]
    private PlayerStateManager _state;
    private Vector3 _targetDirection = Vector3.zero;
    private Vector3 _targetPos = Vector3.zero;
    private bool _canRush = false;
    private bool _isTouchTheEnemy = false;
    public void SetTargetAndRushStart(Transform target)
    {
        _canRush = true;
        _targetDirection = (target.position- _onBallRigidBody.position).normalized;
        _targetPos = FinalDestination(_targetDirection, _dashSpeed);
        _state.ChangeLeftAttackState();
        StopRush();
    }
    private void FixedUpdate()
    {
        if (!_canRush)
        {
            return;
        }
        _onBallRigidBody.MovePosition(Vector3.MoveTowards(_onBallRigidBody.position, _targetPos, _dashSpeed * Time.fixedDeltaTime));
    }

    private async void StopRush()
    {
        await UniTask.WaitForSeconds(_rushTime);
        _canRush = false;
        if (!_isTouchTheEnemy)
        {
            _state.ChangeNormalState();
            _anim.LeftATKProtocol();
        }
        _isTouchTheEnemy= false;
    }

    private Vector3 FinalDestination(Vector3 direction,float moveSpeed)
    {
        Vector3 finalDestination = _onBallRigidBody.position + direction * _dashSpeed * _rushTime;
        return finalDestination;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            _isTouchTheEnemy = true;
            _anim.LeftATKProtocol();
            _state.ChangeNormalState();
            EnemyToDamageProtocol(obj);
        }
    }

    private void EnemyToDamageProtocol(GameObject enemy)
    {
        EnemyTakeDamage enDamage = enemy.GetComponent<EnemyTakeDamage>();
        if(enDamage == null)
        {
            return;
        }

        enDamage.TakeDamage(_targetDirection);
    }
}
