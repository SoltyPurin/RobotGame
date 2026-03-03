using UnityEngine;

public class PracticeManager : MonoBehaviour
{
    [SerializeField, Header("—ûK—p‚Ì“GƒvƒŒƒnƒu")]
    private GameObject _practiceEnemyPrefab = default;
    [SerializeField, Header("“G‚ª—N‚­À•W")]
    private Vector3 _enPos = new Vector3(131f, 115f, 67f);

    private void Start()
    {
        EnemySpawn();
    }
    public void EnemySpawn()
    {
        GameObject enemy = Instantiate(_practiceEnemyPrefab, _enPos, Quaternion.identity);
        enemy.AddComponent<PracticeEnemyDeathAleart>().SetUp(this);
    }
}
