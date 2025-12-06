using UnityEngine;

public enum EnemyStateTemp
{
    Move,
    Jump,
    RightAttack,
    LeftAttack,
    Dash,
    Think,
}

public class AIControllScript : MonoBehaviour
{
    [SerializeField]
    private EnemyStateTemp _state = EnemyStateTemp.Move;
    public EnemyStateTemp State
    {
        set {  _state = value; }
    }
    private AIMove _move = default;
    private Jump _jump = default;
    private Dash _dash = default;
    [SerializeField,Header("射撃のスクリプト")]
    private RightAttack _rightAttack = default;
    private Vector3 _targetPos = default;
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;
    [SerializeField, Header("アニメーション再生のスクリプト")]
    private PlayAnimationScript _anim = default;
    [SerializeField, Header("一回の射撃で何発撃つか")]
    private int _shootCount = 3;

    private bool _isTargetCalculated = false;

    private GameObject _playerObj = default;

    private void Start()
    {
        _move = GetComponent<AIMove>();
        _jump = GetComponent<Jump>();
        _dash = GetComponent<Dash>();
        _playerObj = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case EnemyStateTemp.Move:
                _move.MoveProtocol(CalcTargetPos(),_nearTargetPosDistance);
                _anim.MoveAnim();
                break;

            case EnemyStateTemp.Jump:
                _jump.JumpProtocol();
                break;

            case EnemyStateTemp.RightAttack:
                for(int i =0; i< _shootCount; i++)
                {
                    _rightAttack.ShootProtocol(_playerObj.transform);
                    _anim.RightAttackAnim();
                }
                _state = EnemyStateTemp .Think;
                break;

            case EnemyStateTemp.LeftAttack:
                break;

            case EnemyStateTemp.Dash:
                _dash.DashProtocol(_targetPos.normalized);
                _anim.JumpAnim();
                _state = EnemyStateTemp.Jump;
                break;  

            default:
                break;
        }
    }

    public void ThinkNextMove()
    {
        Debug.Log("次の行動を考えるよ");
        _isTargetCalculated = false;
        float distance = Vector3.Distance(this.transform.position, _playerObj.transform.position);

    }

    private Vector3 CalcTargetPos()
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
