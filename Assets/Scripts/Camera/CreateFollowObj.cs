using Unity.Cinemachine;
using UnityEngine;

public class CreateFollowObj : MonoBehaviour
{
    [SerializeField, Header("カメラ追従オブジェクト")]
    private GameObject _followObj = default;

    private LockOnEnemy _lockOn = default;
    private CenterObj _centerObjScript = default;

    private CinemachineCamera _camera = default;
    private void Start()
    {
        _camera = GetComponent<CinemachineCamera>();
        GameObject insta =  Instantiate(_followObj);
        _centerObjScript = insta.GetComponent<CenterObj>();
        _camera.LookAt = insta.transform;
        _lockOn = GetComponent<LockOnEnemy>();
        _lockOn.SetCenterObj(_centerObjScript);
        GameObject cameraPoint = _camera.Follow.gameObject;
        MoveCameraPoint movePoint = cameraPoint.GetComponent<MoveCameraPoint>();
        GameObject player = GameObject.FindWithTag("Player");
        movePoint.SetUp(insta, player);
    }
}
