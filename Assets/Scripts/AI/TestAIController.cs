using UnityEngine;
using UnityEngine.InputSystem;

public class TestAIController : MonoBehaviour
{
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("地面の検知")]
    private EnemyDetectGround _detectGround = default;
    [SerializeField, Header("アニメーション再生のスクリプト")]
    private PlayAnimationScript _anim = default;
    [SerializeField, Header("被弾のスクリプト")]
    private TakeDamageScript _takeDamage = default;
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;
    [SerializeField, Header("ダッシュ時の速度")]
    private float _rushSpeed = 40f;
    [SerializeField, Header("突進時間")]
    private float _rushTime = 1.4f;
    [SerializeField, Header("近接で与えるダメージ")]
    private float _meleeDamageValue = 50f;
    [SerializeField, Header("近接攻撃の吹き飛ばし力")]
    private float _meleeBlowAwayPower = 50f;
    [SerializeField, Header("射撃で与えるダメージ")]
    private float _bulletDamageValue = 50f;
    [SerializeField, Header("射撃での吹き飛ばし力")]
    private float _bulletBlowAwayPower = 50;
    [SerializeField, Header("銃弾の生存時間")]
    private float _bulletAliveTime = 5;
    [SerializeField, Header("射撃開始地点")]
    private Transform _shootPoint = default;

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
    [SerializeField, Header("回避力")]
    private float _dogdePower = 10;
    [SerializeField, Header("ジャンプ力")]
    private float _jumpPower = 10;
    [SerializeField, Header("待機状態の待機時間")]
    private float _stopTime = 1;


    private StateMachine _stateMachine; // プレイヤーの状態を管理するStateMachine

    private bool _isTargetCalculated = false;

    private Vector3 _targetPos = default;

    private EnemyContext _ctx;
    private GameObject _playerObj = default;

    private bool _isAttacked = false;

    private void Start()
    {
        _playerObj = GameObject.FindWithTag("Player");
        _ctx = new EnemyContext();
        _ctx.Transform = this.transform;
        _ctx.Controller = this;
        _ctx.MoveSpeed = _aiMoveSpeed;
        _ctx.OnBallRigidbody = _onBallRigidBody;
        _ctx.BallRigidBody = _ballRigidBody;
        _ctx.DodgePower = _dogdePower;
        _ctx.JumpPower = _jumpPower;
        _ctx.StopTime = _stopTime;
        _ctx.Ground = _detectGround;
        _ctx.RushSpeed = _rushSpeed;
        _ctx.RushTime = _rushTime;
        _ctx.Animation = _anim;
        _ctx.MeleeBlowAwayPower = _meleeBlowAwayPower;
        _ctx.MeleeDamage = _meleeDamageValue;
        _ctx.PlayerTransform = _playerObj.transform;
        _ctx.Pool = GameObject.FindWithTag("BulletPool").GetComponent<BulletPool>();
        _ctx.BulletBlowAwayPower = _bulletBlowAwayPower;
        _ctx.BulletAliveTime = _bulletAliveTime;
        _ctx.BulletDamage = _bulletDamageValue;
        _ctx.ShootPoint = _shootPoint;
        _stateMachine = new StateMachine(); // StateMachineのインスタンスを作成
        _stateMachine.ChangeState(new MoveState(),this,_ctx); // 初期状態を設定
    }

    private void Update()
    {
        _ctx.PlayerTransform = _playerObj.transform;
        Vector3 eur = transform.eulerAngles;
        eur.x = 0;
        eur.z = 0;
        transform.rotation = Quaternion.Euler(eur);
        if (_takeDamage.IsBlowning)
        {
            return;
        }
        _stateMachine.FixedUpdate(); 
        _ctx.Transform = this.transform;

    }

    public void ThinkNextMove()
    {
        _isTargetCalculated = false;
        float distance = CalcTargetDistance();

        if (_isAttacked)
        {
            if(distance > 20)
            {
                _stateMachine.ChangeState(new MoveState(), this, _ctx);
            }
            else
            {
                _stateMachine.ChangeState(new JumpState(), this, _ctx);
            }
            _isAttacked = false;
        }
        else
        {
            //if (distance > 50)
            //{
            //    _stateMachine.ChangeState(new RightAttackState(), this, _ctx);
            //}
            //else
            //{
                _stateMachine.ChangeState(new LeftAttackState(), this, _ctx);
            //}
            _isAttacked = true;
        }


        //if(distance >20 && distance < 10)
        //{

        //}

        //if (_isJumpCallOnce)
        //{
        //    stateMachine.ChangeState(new MoveState(), this, _ctx);
        //    _anim.MoveAnim();
        //}
        //else
        //{
        //    stateMachine.ChangeState(new JumpState(), this, _ctx);
        //    _anim.JumpAnim();
        //    _isJumpCallOnce = true;
        //}
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

    private float CalcTargetDistance()
    {
        float distance = Vector3.Distance(_playerObj.transform.position, this.transform.position);
        return distance;
    }
    private void OnDrawGizmos()
    {
        // 再生中しか意味がないのでガード
        if (!Application.isPlaying) return;

        // ターゲット座標
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_targetPos, 0.5f);

        // 現在位置 → ターゲットへの線
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _targetPos);
    }
}
