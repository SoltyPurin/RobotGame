using UnityEngine;

public class LockOnMarkerViewer : MonoBehaviour
{
    [SerializeField, Header("ロックオンマーカー")]
    private RectTransform _lockOnMarker = default;
    [SerializeField, Header("シネマシンじゃない方のカメラ")]
    private Camera _camera = default;

    private Transform _target = default;
    private Vector3 _targetScreenPos = default;

    public void SetLockOnMarkar(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        Vector3 cameraDir = _camera.transform.forward;
        Vector3 targetWorldPos = _target.position;
        Vector3 targetDir = targetWorldPos-_camera.transform.position;

        bool isFront = Vector3.Dot(cameraDir,targetDir) > 0;
        _lockOnMarker.gameObject.SetActive(isFront);
        if (!isFront)
        {
            return;
        }
        _targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_lockOnMarker, _targetScreenPos, null, out Vector2 uiLocalPos);
        _lockOnMarker.localPosition = uiLocalPos;
    }
}
