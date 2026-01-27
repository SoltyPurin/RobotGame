using Unity.Cinemachine;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    [SerializeField, Header("€–S‚ÌƒJƒƒ‰")]
    private CinemachineCamera _deathCamera = default;
    public void MoveDeathObject(GameObject enemyObj,CinemachineCamera curCamera)
    {
        Vector3 enPos = enemyObj.transform.position;
        transform.position = enPos;
        curCamera.Priority = 0;
        _deathCamera.Priority = 1;
    }
}
