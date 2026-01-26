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
        _currentEnemyIndex = -1;
    }
    public GameObject SearchAndReturnNearEnemy()
    {
        if (_enemys == null || _enemys.Length == 0)
        {
            return null;
        }

        for (int i = 0; i < _enemys.Length; i++)
        {
            _currentEnemyIndex = (_currentEnemyIndex + 1) % _enemys.Length;
            GameObject candidate = _enemys[_currentEnemyIndex];

            if (candidate != null)
            {
                return candidate;
            }
        }

        return null;
    }
}
