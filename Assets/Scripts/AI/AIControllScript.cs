using UnityEngine;

public enum EnemyState
{
    Move,
    Jump,
    Attack,
    Dash,
}

public class AIControllScript : MonoBehaviour
{
    private EnemyState _state = EnemyState.Move;
    public EnemyState State
    {
        set {  _state = value; }
    }
    private AIMove _move = default;
    private Jump _jump = default;
    private Dash _dash = default;
    private Vector3 _targetPos = default;
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;

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
            case EnemyState.Move:
                _move.MoveProtocol(CalcTargetPos(),_nearTargetPosDistance);
                break;

            case EnemyState.Jump:
                _jump.JumpProtocol();
                break;

            case EnemyState.Attack:
                break;

            case EnemyState.Dash:
                _dash.DashProtocol(_targetPos.normalized);
                _state = EnemyState.Jump;
                break;  
        }
    }

    public void ThinkNextMove()
    {
        Debug.Log("次の行動を考えるよ");
        _isTargetCalculated = false;
        float distance = Vector3.Distance(this.transform.position, _playerObj.transform.position);
        if(distance < 10)
        {
            _state = EnemyState.Dash;
            Debug.Log("敵のステートはダッシュ");
        }
        else
        {
            _state = EnemyState.Move;
            Debug.Log("敵のステートは移動");
        }
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
