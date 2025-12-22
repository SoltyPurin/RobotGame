using UnityEngine;

public class EnemyStackDetect : MonoBehaviour
{
    private float _stackAcceptTime = 0.5f;
    private float _currentTime = 0;
    private TestAIController _controller = default;

    private Vector3 _prevPos = Vector3.zero;

    private void Start()
    {
        _prevPos = transform.position;
        _controller = GetComponent<TestAIController>();
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if( _currentTime >= _stackAcceptTime)
        {
            _currentTime = 0;
            Vector3 curPos = transform.position;
            CheckStacking(curPos);
        }
    }

    private void CheckStacking(Vector3 curPos)
    {
        float distance = Vector3.Distance(_prevPos, curPos);
        if(distance < 10)
        {
            Debug.Log("スタック検知");
            _controller.ThinkNextMove();
        }
        _prevPos = curPos;
    }
}
