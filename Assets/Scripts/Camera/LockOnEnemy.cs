using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnEnemy : MonoBehaviour
{
    private GameObject[] _enemys = default;
    private InputAction _lockOnButton;
    private CinemachineCamera _cineCamera = default;
    private bool _isLockOn = false;
    private bool _isSetCalled = false;

    private CenterObj _centerObj;
    public void SetCenterObj(in CenterObj script)
    {
        _centerObj = script;
    }

    private void Start()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _cineCamera = GetComponent<CinemachineCamera>();
        _lockOnButton = InputSystem.actions.FindAction("LockOn");
    }
    private void LateUpdate()
    {
        if (_lockOnButton.WasPressedThisFrame())
        {
            _isLockOn = !_isLockOn;
        }

        if (!_isLockOn)
        {
            _isSetCalled = false;
            transform.rotation = Quaternion.Euler(10,0,0);  
            return;
        }
        if (!_isSetCalled)
        {
            _isSetCalled = true;
            Debug.Log("ÉçÉbÉNÉIÉì");
            SetTartgetProtocol();
        }
    }

    private void SetTartgetProtocol()
    {
        _cineCamera.LookAt = _enemys[0].transform;
        _centerObj.SetTarget(_enemys[0]);
        //_camera.transform.LookAt(_enemys[0].transform);
    }
}
