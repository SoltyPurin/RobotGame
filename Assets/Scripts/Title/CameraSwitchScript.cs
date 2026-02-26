using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CameraSwitchScript : MonoBehaviour
{
    private Stack<CinemachineCamera> _camera = new Stack<CinemachineCamera>();
    private CinemachineCamera _currentCamera;

    public void StuckIn(CinemachineCamera prev,CinemachineCamera next)
    {
        Debug.Log("カメラ切り替え");
        _camera.Push(prev);
        _currentCamera = next;
        prev.Priority = 0;
        next.Priority = 1;
    }

    public void StuckOut()
    {
        Debug.Log("スタック戻し");
        _currentCamera.Priority = 0;
        if( _camera.Count > 0)
        {
            _currentCamera = _camera.Pop();
        }
        _currentCamera.Priority = 1;
    }
}
