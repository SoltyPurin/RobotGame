using UnityEngine;

public class IdleState : MonoBehaviour,IEnemyState
{
    [SerializeField, Header("’âŽ~ŽžŠÔ")]
    private float _stopTime = 1;
    private float _currentTime = 0;
    public void Enter(in TestAIController controller,in EnemyContext ctx)
    {

    }

    public void Update()
    {
        Debug.Log("’âŽ~’†");
        _currentTime += Time.deltaTime;
        if( _currentTime > _stopTime)
        {
            Exit();
        }
    }

    public void Exit() 
    {

    }
}
