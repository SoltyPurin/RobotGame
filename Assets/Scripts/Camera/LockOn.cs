using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public enum CameraState
{
    Normal,
    LockOn,
}

public class LockOn : MonoBehaviour
{
    [SerializeField, Header("通常カメラ")]
    private CinemachineCamera _normalCamera = default;
    [SerializeField, Header("ロックオンカメラ")]
    private CinemachineCamera _lockOnCamera = default;
    [SerializeField, Header("ロックオンしてない時の注視オブジェクト")]
    private Transform _notLockOnObject = default;

    private SearchNearEnemy _nearEnemy = default;
    private Transform _targetTransform = default;
    private CameraState _cameraState = CameraState.Normal;
    public CameraState State
    {
        get { return _cameraState; }    
    }

    private void Start()
    {
        _nearEnemy = this.gameObject.AddComponent<SearchNearEnemy>();
        _targetTransform = _notLockOnObject;
    }


    public void ChangeCamera()
    {
        switch (_cameraState)
        {
            case CameraState.Normal:
                _cameraState = CameraState.LockOn;
                _targetTransform = _nearEnemy.SearchAndReturnNearEnemy().transform;
                _lockOnCamera.LookAt = _targetTransform;
                _normalCamera.Priority = 0;
                break;

            case CameraState.LockOn:
                _cameraState = CameraState.Normal;
                _targetTransform = _notLockOnObject;
                _normalCamera.Priority = 10;
                break;  
        }
    }

    public Transform CurrentTargetObject()
    {
        return _targetTransform;
    }
}
