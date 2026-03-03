using UnityEngine;

public class PracticeEnemyDeathAleart : MonoBehaviour
{
    private PracticeManager _manager = default;

    private EnemyTakeDamage _damage = default;
    public void SetUp(PracticeManager manager)
    {
        _manager = manager;
        _damage = GetComponent<EnemyTakeDamage>();
    }

    public void DeathProtocol()
    {
        _manager.EnemySpawn();
    }
}
