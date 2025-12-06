using UnityEngine;
using UnityEngine.InputSystem;

public class TestAIController : MonoBehaviour
{
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;
    public float NearTargetPosDistance
    {
        get { return _nearTargetPosDistance; }
    }
    [SerializeField, Header("移動速度")]
    private float _aiMoveSpeed = 50f;
    public float AIMoveSpeed
    {
        get { return _aiMoveSpeed; }
    }
    [SerializeField, Header("ダッシュ力")]
    private float _dashPower = 10;


    private StateMachine stateMachine; // プレイヤーの状態を管理するStateMachine

    private bool _isTargetCalculated = false;

    private Vector3 _targetPos = default;

    private EnemyContext _ctx;



    private void Start()
    {
        _ctx = new EnemyContext();
        _ctx.Transform = this.transform;
        _ctx.Controller = this;
        _ctx.MoveSpeed = _aiMoveSpeed;
        _ctx.OnBallRigidbody = _onBallRigidBody;
        _ctx.BallRigidBody = _ballRigidBody;
        _ctx.DashPower = _dashPower;
        stateMachine = new StateMachine(); // StateMachineのインスタンスを作成
        stateMachine.ChangeState(new MoveState(),this,_ctx); // 初期状態を設定
    }

    private void Update()
    {
        stateMachine.Update(); // 現在の状態のUpdateメソッドを呼び出す
        _ctx.Transform = this.transform;

    }

    public void ThinkNextMove()
    {
        _isTargetCalculated = false;
        Debug.Log("次の行動を考える");
    }

    public Vector3 CalcTargetPos()
    {
        if (_isTargetCalculated)
        {
            return _targetPos;
        }
        Vector3 curPos = this.transform.position;
        curPos.x += Random.Range(-_moveMaxDistance, _moveMaxDistance);
        curPos.z += Random.Range(-_moveMaxDistance, _moveMaxDistance);
        _isTargetCalculated = true;
        _targetPos = curPos;
        return curPos;
    }

}
