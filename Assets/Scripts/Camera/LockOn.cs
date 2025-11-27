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

    private InputAction _lockOnButton = default;
    private SearchNearEnemy _nearEnemy = default;
    private CameraState _cameraState = CameraState.Normal;
    public CameraState State
    {
        get { return _cameraState; }    
    }

    private void Start()
    {
        _lockOnButton = InputSystem.actions.FindAction("LockOn");
        _nearEnemy = this.gameObject.AddComponent<SearchNearEnemy>();
    }

    private void Update()
    {
        if (_lockOnButton.WasPressedThisFrame())
        {
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        Debug.Log("視点切り替え");
        switch (_cameraState)
        {
            case CameraState.Normal:
                _cameraState = CameraState.LockOn;
                _lockOnCamera.LookAt = _nearEnemy.SearchAndReturnNearEnemy().transform;
                _normalCamera.Priority = 0;
                break;

            case CameraState.LockOn:
                _cameraState = CameraState.Normal;
                _normalCamera.Priority = 10;
                break;  
        }
    }
}
