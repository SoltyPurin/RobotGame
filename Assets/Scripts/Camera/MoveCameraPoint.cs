using UnityEngine;

public class MoveCameraPoint : MonoBehaviour
{
    private GameObject _playerObj = default;
    private GameObject _centerObj = default;
    public void SetUp(GameObject centerObj,GameObject player)
    {
        _centerObj = centerObj;
        _playerObj = player;
    }
}
