using UnityEngine;

public class SearchNearEnemy : MonoBehaviour
{
    private GameObject[] _enemys;
    private int _currentEnemyIndex = 0;
    private void Awake()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public void ChangeEnemyArray()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _currentEnemyIndex = 0;
    }
    public GameObject SearchAndReturnNearEnemy()
    {
        _currentEnemyIndex++;
        if(_currentEnemyIndex >= _enemys.Length)
        {
            _currentEnemyIndex = 0;
        }

        return _enemys[_currentEnemyIndex];
    }
}
