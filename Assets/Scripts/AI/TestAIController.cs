using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAIController : MonoBehaviour
{
    [SerializeField, Header("どれくらい弾が近づいたら回避するか")]
    private float _dodgeRange = 5f;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("地面の検知")]
    private EnemyDetectGround _detectGround = default;
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;
    [SerializeField, Header("ダッシュ時の速度")]
    private float _rushSpeed = 40f;
    [SerializeField, Header("突進時間")]
    private float _rushTime = 1.4f;
    [SerializeField, Header("近接で与えるダメージ")]
    private int _meleeDamageValue = 50;
    [SerializeField, Header("近接攻撃の吹き飛ばし力")]
    private float _meleeBlowAwayPower = 50f;
    [SerializeField, Header("射撃で与えるダメージ")]
    private int _bulletDamageValue = 50;
    [SerializeField, Header("射撃での吹き飛ばし力")]
    private float _bulletBlowAwayPower = 50;
    [SerializeField, Header("銃弾の生存時間")]
    private float _bulletAliveTime = 5;
    [SerializeField, Header("重力")]
    private float _gravity = 150;
    [SerializeField, Header("射撃開始地点")]
    private Transform _shootPoint = default;
    [SerializeField, Header("近接攻撃の判定の大きさ")]
    private Vector3 _meleeRangeSize;
    [SerializeField, Header("近接判定の中心座標")]
    private Vector3 _meleeRangeCenter;

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
    private float _dodgePower = 10;
    [SerializeField, Header("回避時間")]
    private float _dodgeTime = 0.5f;
    [SerializeField, Header("ジャンプ力")]
    private float _jumpPower = 10;
    [SerializeField, Header("待機状態の待機時間")]
    private float _stopTime = 1;


    private StateMachine _stateMachine; // プレイヤーの状態を管理するStateMachine

    private bool _isTargetCalculated = false;

    private Vector3 _targetPos = default;

    private EnemyContext _ctx;
    private GameObject _playerObj = default;
    private TakeDamageScript _takeDamage = default;
    private PlayAnimationScript _anim = default;

    private bool _isAttacked = false;

    private void Start()
    {
        _takeDamage = GetComponent<TakeDamageScript>();
        _anim = GetComponent<PlayAnimationScript>();
        _playerObj = GameObject.FindWithTag("Player");
        _ctx = new EnemyContext();
        _ctx.Transform = this.transform;
        _ctx.Controller = this;
        _ctx.MoveSpeed = _aiMoveSpeed;
        _ctx.OnBallRigidbody = _onBallRigidBody;
        _ctx.BallRigidBody = _ballRigidBody;
        _ctx.DodgePower = _dodgePower;
        _ctx.DodgeTime = _dodgeTime;
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
        _ctx.Gravity = _gravity;
        _ctx.MeleeRangeSize = _meleeRangeSize;
        _ctx.MeleeRangeCenter = _meleeRangeCenter;
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
        if (IsNearBullet())
        {
            Debug.Log("回避");
            _stateMachine.ChangeState(new DashState(), this, _ctx);
        }


        if (_isAttacked)
        {
            if (distance > 20)
            {
                Debug.Log("移動");
                _stateMachine.ChangeState(new MoveState(), this, _ctx);
            }
            else
            {
                Debug.Log("ジャンプ");
                _stateMachine.ChangeState(new JumpState(), this, _ctx);
            }
            _isAttacked = false;
        }
        else
        {
            ShootAsync().Forget();
            _isAttacked = true;
        }
    }

    private async UniTaskVoid ShootAsync()
    {
        for(int i = 0; i < 3; i++)
        {
            _stateMachine.ChangeState(new RightAttackState(), this, _ctx);
            await UniTask.Delay(1);
        }
    }

    private bool IsNearBullet()
    {
        GameObject[] plBullets = GameObject.FindGameObjectsWithTag("PLBullet");
        if(plBullets.Length == 0)
        {
            return false;
        }
        float nearDistance = Vector3.Distance(plBullets[0].transform.position,transform.position);
        for(int i = 1; i < plBullets.Length; i++)
        {
            float calcDistance = Vector3.Distance(plBullets[i].transform.position, transform.position);
            if(nearDistance > calcDistance)
            {
                nearDistance = calcDistance;
            }
        }

        if(nearDistance < _dodgeRange)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        curPos = ReturnGroundPos(curPos.x, curPos.z);
        _isTargetCalculated = true;
        _targetPos = curPos;
        return curPos;
    }

    private float CalcTargetDistance()
    {
        float distance = Vector3.Distance(_playerObj.transform.position, this.transform.position);
        return distance;
    }

    private Vector3 ReturnGroundPos(float x,float z)
    {
        RaycastHit hit;
        Vector3 startPoint = new Vector3(x,transform.position.y + 1,z);
        Physics.Raycast(startPoint,Vector3.down, out hit, Mathf.Infinity);
        startPoint = hit.point;
        return startPoint;
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


        Vector3 target = _playerObj.transform.position;
        Vector3 targetDir = (target - _onBallRigidBody.position).normalized;
        Vector3 attackRangeCenter = _onBallRigidBody.transform.position;
        attackRangeCenter.y += _meleeRangeCenter.y;
        attackRangeCenter.z += _meleeRangeCenter.z;

        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(
            attackRangeCenter + targetDir * 1f,
            _meleeRangeSize);

    }
}
