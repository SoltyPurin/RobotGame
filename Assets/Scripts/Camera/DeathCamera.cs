using Unity.Cinemachine;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    [SerializeField, Header("éÄñSéûÇÃÉJÉÅÉâ")]
    private CinemachineCamera _deathCamera = default;
    private GameObject _enPos = default;

    private bool _isDestroying = false;
    public void MoveDeathObject(GameObject enemyObj,CinemachineCamera curCamera)
    {
        _enPos = enemyObj;
        curCamera.Priority = 0;
        _deathCamera.Priority = 1;
        _isDestroying=true;
        //Time.timeScale = 0.5f;
    }

    private void Update()
    {
        if (!_isDestroying)
        {
            return;
        }
        transform.position = _enPos.transform.position;
    }
}
