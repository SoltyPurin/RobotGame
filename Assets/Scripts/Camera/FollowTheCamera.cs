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

    private void LateUpdate()
    {
        Vector3 decisionPos = _followObj.transform.position;
        decisionPos.y += _cameraPos.y;
        decisionPos.z += _cameraPos.z;  
        transform.position = decisionPos;
    }
}
