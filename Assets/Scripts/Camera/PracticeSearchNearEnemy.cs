using UniRx.Examples;
using UnityEngine;

public class PracticeSearchNearEnemy : SearchNearEnemy
{
    public override GameObject SearchAndReturnNearEnemy()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");

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
