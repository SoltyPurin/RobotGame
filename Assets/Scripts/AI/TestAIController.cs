using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
public enum EnemyType
{
    Melee,
    ShotWeapon,
    Normal,
}

public class TestAIController : MonoBehaviour
{
    [SerializeField, Header("ScriptableObject")]
    private EnemyStatus _status = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField,Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("地面の検知")]
    private EnemyDetectGround _detectGround = default;
    [SerializeField, Header("近接攻撃の判定の大きさ")]
    private Vector3 _meleeRangeSize;
    [SerializeField, Header("近接判定の中心座標")]
    private Vector3 _meleeRangeCenter;
    [SerializeField, Header("射撃開始地点")]
    private Transform _shootPoint = default;
    private float _nearTargetPosDistance = 5;


    public float NearTargetPosDistance
    {
        get { return _nearTargetPosDistance; }
    }
    private float _aiMoveSpeed;
    public float AIMoveSpeed
    {
        get { return _aiMoveSpeed; }
    }
    private StateMachine _stateMachine; // プレイヤーの状態を管理するStateMachine

    private bool _isTargetCalculated = false;

    private Vector3 _targetPos = default;

    private EnemyContext _ctx;
    private GameObject _playerObj = default;
    private TakeDamageScript _takeDamage = default;
    private PlayAnimationScript _anim = default;
    private Rigidbody _plRigidBody = default;

    private bool _isAttacked = false;

    private int _moveCount = 0;

    private bool _isAlive = true;

    private void Start()
    {
        _takeDamage = GetComponent<TakeDamageScript>();
        _anim = GetComponent<PlayAnimationScript>();
        _playerObj = GameObject.FindWithTag("Player");
        _plRigidBody = _playerObj.GetComponent<Rigidbody>();
        _nearTargetPosDistance = _status.NearTargetPosDistance;
        _aiMoveSpeed = _status.AiMoveSpeed;
        _ctx = new EnemyContext();
        _ctx.Transform = this.transform;
        _ctx.Controller = this;
        _ctx.MoveSpeed = _aiMoveSpeed;
        _ctx.OnBallRigidbody = _onBallRigidBody;
        _ctx.BallRigidBody = _ballRigidBody;
        _ctx.DodgePower = _status.DodgePower;
        _ctx.DodgeTime = _status.DodgeTime;
        _ctx.JumpPower = _status.JumpPower;
        _ctx.StopTime = _status.StopTime;
        _ctx.Ground = _detectGround;
        _ctx.RushSpeed = _status.RushSpeed;
        _ctx.RushTime = _status.RushTime;
        _ctx.Animation = _anim;
        _ctx.MeleeBlowAwayPower = _status.MeleeBlowAwayPower;
        _ctx.MeleeDamage = _status.MeleeDamageValue;
        _ctx.PlayerTransform = _playerObj.transform;
        _ctx.PlayerPosition = _playerObj.transform.position;
        _ctx.Pool = GameObject.FindWithTag("BulletPool").GetComponent<BulletPool>();
        _ctx.BulletBlowAwayPower = _status.BulletBlowAwayPower;
        _ctx.BulletAliveTime = _status.BulletAliveTime;
        _ctx.BulletDamage = _status.BulletDamageValue;
        _ctx.ShootPoint = _shootPoint;
        _ctx.Gravity = _status.Gravity;
        _ctx.MeleeRangeSize = _meleeRangeSize;
        _ctx.MeleeRangeCenter = _meleeRangeCenter;
        _stateMachine = new StateMachine(); // StateMachineのインスタンスを作成
        _stateMachine.ChangeState(new MoveState(), this, _ctx); // 初期状態を設定

    }

    private void FixedUpdate()
    {
        if (!_isAlive)
        {
            return;
        }
        _ctx.PlayerTransform = _playerObj.transform;
        _ctx.PlayerPosition = PredictionPlayerPos();
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

    public void Dead()
    {
        _isAlive = false;
    }

    public void ThinkNextMove()
    {
        _isTargetCalculated = false;
        float distance = CalcTargetDistance();
        if (IsNearBullet())
        {
            Debug.Log("回避");
            _stateMachine.ChangeState(new DashState(), this, _ctx);
            return;
        }

        if (_isAttacked)
        {
            MoveThinkProtocol(distance);
        }
        else
        {
            AttackThinkProtocol(distance);
        }

    }

    public void AttackThinkProtocol(float distance)
    {
        switch (_status.EnemyType)
        {
            case EnemyType.Melee:
                _stateMachine.ChangeState(new LeftAttackState(), this, _ctx);
                break;

            case EnemyType.ShotWeapon:
                _stateMachine.ChangeState(new RightAttackState(), this, _ctx);
                break;

            case EnemyType.Normal:
                NormalEnemyThink(distance);
                break;
        }

        _isAttacked = true;
    }

    private void NormalEnemyThink(float distance)
    {
        if (distance > _status.MeleeAttackRange)
        {
            Debug.Log("射撃");
            _stateMachine.ChangeState(new RightAttackState(), this, _ctx);
        }
        else
        {
            Debug.Log("近接");
            _stateMachine.ChangeState(new LeftAttackState(), this, _ctx);
        }
    }

    private void MoveThinkProtocol(float distance)
    {
        if (distance > 20)
        {
            if (_moveCount < _status.MoveCountUntilJump)
            {
                Debug.Log("移動");
                _stateMachine.ChangeState(new MoveState(), this, _ctx);
                _moveCount++;
            }
            else
            {
                Debug.Log("ジャンプ");
                _stateMachine.ChangeState(new JumpState(), this, _ctx);
                _moveCount = 0;
            }
        }
        else
        {
            Debug.Log("ジャンプ");
            _stateMachine.ChangeState(new JumpState(), this, _ctx);
        }
        _isAttacked = false;
    }

    /// <summary>
    /// プレイヤーの移動を先読みする
    /// </summary>
    /// <returns>特定秒数後のプレイヤーの座標予測地点</returns>
    private Vector3 PredictionPlayerPos()
    {
        float speed = _plRigidBody.linearVelocity.magnitude;
        if(speed < _status.SakiyomiStopSpeed)
        {
            Debug.Log("先読み辞めてます");
            return _playerObj.transform.position;
        }
        else
        {
            Debug.Log("先読み中");
            Vector3 moveDir = _plRigidBody.linearVelocity;
            Vector3 plPos = _playerObj.transform.position;
            Vector3 sakiyomiPos = plPos + moveDir * _status.SakiyomiTime;
            return sakiyomiPos;

        }
    }

    /// <summary>
    /// 銃弾が近いかを判断する
    /// </summary>
    /// <returns>銃弾が近いか</returns>
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

        if(nearDistance < _status.DodgeRange)
        {
            Debug.Log("銃弾が近いよ");
            return true;
        }
        else
        {
            Debug.Log("銃弾が遠いよ");
            return false;
        }
    }

    /// <summary>
    /// 目標地点を計算するスクリプト。目的地に着くまでは再計算しない
    /// </summary>
    /// <returns>目標地点の座標</returns>
    public Vector3 CalcTargetPos()
    {
        if (_isTargetCalculated)
        {
            return _targetPos;
        }
        float moveMaxDistance = _status.MoveMaxDistance;
        Vector3 curPos = this.transform.position;
        curPos.x += Random.Range(-moveMaxDistance,moveMaxDistance);
        curPos.z += Random.Range(-moveMaxDistance, moveMaxDistance);
        curPos = ReturnGroundPos(curPos.x, curPos.z);
        curPos = ClampDestinationToStage(curPos);
        _isTargetCalculated = true;
        _targetPos = curPos;
        return curPos;
    }

    public float CalcTargetDistance()
    {
        float distance = Vector3.Distance(_playerObj.transform.position, this.transform.position);
        return distance;
    }

    private Vector3 ReturnGroundPos(float x,float z)
    {
        RaycastHit underHit;
        RaycastHit upHit;
        Vector3 startPoint = new Vector3(x,transform.position.y + 1,z);
        bool isHitDown = Physics.Raycast(startPoint,Vector3.down, out underHit, Mathf.Infinity);
        bool isHitUp = Physics.Raycast(startPoint,Vector3.up, out upHit, Mathf.Infinity);
        if (isHitDown)
        {
            startPoint = underHit.point;
        }
        else if (isHitUp)
        {
            startPoint = upHit.point;
        }
        return startPoint;
    }

    private Vector3 ClampDestinationToStage(Vector3 destination)
    {
        float xPos = destination.x;
        float zPos = destination.z;
        bool isXFits = xPos < 490 && xPos > -490;
        bool isZFits = zPos < 490 && zPos > -490;
        bool isOk = isXFits && isZFits;
        if (isOk)
        {
            Debug.Log("再計算したが問題なし");
            return destination;
        }
        else
        {
            Debug.Log("目標座標逸脱、座標再修正");
            float moveMaxDistance = _status.MoveMaxDistance;
            Vector3 curPos = transform.position;
            curPos.x += Random.Range(-moveMaxDistance, moveMaxDistance);
            curPos.z += Random.Range(-moveMaxDistance, moveMaxDistance);
            curPos = ReturnGroundPos(curPos.x, curPos.z);
            curPos.x = Mathf.Clamp(curPos.x, -490, 490);
            curPos.z = Mathf.Clamp(curPos.z, -490, 490);
            return curPos;


        }
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
