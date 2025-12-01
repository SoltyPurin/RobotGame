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

    private void Start()
    {
        _move = GetComponent<AIMove>();
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case EnemyState.Move:
                _move.MoveProtocol(new Vector3(90,10,10), 20);
                break;

            case EnemyState.Jump:
                break;

            case EnemyState.Attack:
                break;

            case EnemyState.Dash:
                break;  
        }
    }

    public void ThinkNextMove()
    {

    }
}
