using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnEnemy : MonoBehaviour
{
    private GameObject[] _enemys = default;
    private InputAction _lockOnButton;
    private GameObject _camera = default;
    private bool _isLockOn = false;

    private void Start()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _camera = GameObject.FindWithTag("MainCamera");
        if( _camera == null)
        {
            Debug.Log("カメラが見当たらない");
        }
        else
        {
            Debug.Log("カメラはある");
        }
            _lockOnButton = InputSystem.actions.FindAction("LockOn");
    }
    private void Update()
    {
        if (_lockOnButton.WasPressedThisFrame())
        {
            _isLockOn = !_isLockOn;
        }

        if (!_isLockOn)
        {
            _camera.transform.rotation = Quaternion.Euler(10,0,0);  
            return;
        }

        Debug.Log("ロックオン");
        SetTartgetProtocol();
    }

    private void SetTartgetProtocol()
    {
        _camera.transform.LookAt(_enemys[0].transform);
    }
}
