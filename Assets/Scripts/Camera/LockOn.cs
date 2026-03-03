using Unity.Cinemachine;
using UnityEngine;
public enum CameraState
{
    Normal,
    LockOn,
}

public class LockOn : MonoBehaviour
{
    [SerializeField, Header("ロックオンカメラ")]
    protected CinemachineCamera _lockOnCamera = default;

    protected LockOnMarkerViewer _lockOnMarker = default;
    protected SearchNearEnemy _nearEnemy = default;
    protected Transform _targetTransform = default;
    public Transform TargetTransform
    {
        get { return _targetTransform; }
    }

    public virtual void Initialize()
    {
        _lockOnMarker = FindAnyObjectByType<LockOnMarkerViewer>();
        _nearEnemy = this.gameObject.AddComponent<SearchNearEnemy>();
        _targetTransform = _nearEnemy.SearchAndReturnNearEnemy().transform;
        _lockOnCamera.LookAt = _targetTransform;
        _lockOnMarker.SetLockOnMarkar(_targetTransform);
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


    public virtual void ChangeCamera()
    {
        GameObject enemy = _nearEnemy.SearchAndReturnNearEnemy();
        if (enemy == null)
        {
            UnlockTarget();
            return;
        }
        _targetTransform = enemy.transform;
        _lockOnCamera.LookAt = _targetTransform;
        _lockOnMarker.SetLockOnMarkar(_targetTransform);
    }

    public Transform CurrentTargetObject()
    {
        if (_targetTransform == null)
        {
            return null;
        }
        else
        {
            return _targetTransform;
        }
    }
}
