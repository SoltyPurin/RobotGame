using UnityEngine;

public class IdleState : MonoBehaviour,IEnemyState
{
    private float _stopTime;
    private float _currentTime = 0;

    private EnemyContext _ctx = default;
    private TestAIController _controller = default;
    public void Enter(in TestAIController controller,in EnemyContext ctx)
    {
        _ctx = ctx;
        _controller = controller;
        _stopTime = _ctx.StopTime;
    }

    public void Update()
    {
        Debug.Log("’âŽ~’†");
        _currentTime += Time.deltaTime;
        if( _currentTime > _stopTime)
        {
            _controller.ThinkNextMove();
        }
    }

    public void Exit() 
    {

    }
}
