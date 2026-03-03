using System.Collections;
using UnityEngine;

public class PracticeModeLockOn : LockOn
{
    public override void Initialize()
    {
        _lockOnMarker = FindAnyObjectByType<LockOnMarkerViewer>();
        _nearEnemy = this.gameObject.AddComponent<PracticeSearchNearEnemy>();
        _targetTransform = _nearEnemy.SearchAndReturnNearEnemy().transform;
        _lockOnCamera.LookAt = _targetTransform;
        _lockOnMarker.SetLockOnMarkar(_targetTransform);
    }


}
