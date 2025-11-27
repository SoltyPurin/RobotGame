using UnityEngine;

public class FollowTheCamera : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";

    [SerializeField,Header("プレイヤーからどの地点に常に居続けるか")]
    private Vector3 _cameraPos = Vector3.zero;
    private GameObject _followObj = default;

    private void Start()
    {
        _followObj = GameObject.FindWithTag(PLAYER_TAG);
    }

    public void CallLateUpdate(Vector3 enPos)
    {
        Vector3 direction = (enPos - _followObj.transform.position).normalized;
        Vector3 decisionPos = _followObj.transform.position;

        if (enPos.z > _followObj.transform.position.z)
        {
            decisionPos.z += _cameraPos.z;
        }
        else
        {
            decisionPos.z -= _cameraPos.z;
        }
        decisionPos.y += _cameraPos.y;
        Vector3 pos = direction + _cameraPos + decisionPos;
        transform.position = pos;

    }
}
