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
    [SerializeField, Header("ロックオンカメラ")]
    private CinemachineCamera _lockOnCamera = default;
    [SerializeField, Header("ロックオンしてない時の注視オブジェクト")]
    private Transform _notLockOnObject = default;

    private SearchNearEnemy _nearEnemy = default;
    private Transform _targetTransform = default;
    public Transform TargetTransform
    {
        get { return _targetTransform; }
    }
    private CameraState _cameraState = CameraState.LockOn;
    public CameraState State
    {
        get { return _cameraState; }    
    }

    private void Start()
    {
        _nearEnemy = this.gameObject.AddComponent<SearchNearEnemy>();
        _targetTransform = _nearEnemy.SearchAndReturnNearEnemy().transform;
        Debug.Log("ターゲットは" + _targetTransform.name);
        _lockOnCamera.LookAt = _targetTransform;

    }

    public void UnlockTarget()
    {
        _targetTransform = null;
        _lockOnCamera.LookAt = null;
    }

    public void ReSearch()
    {
        _nearEnemy.ChangeEnemyArray();
    }


    public void ChangeCamera()
    {
        _targetTransform = _nearEnemy.SearchAndReturnNearEnemy().transform;
        _lockOnCamera.LookAt = _targetTransform;
    }

    public Transform CurrentTargetObject()
    {
        return _targetTransform;
    }
}
