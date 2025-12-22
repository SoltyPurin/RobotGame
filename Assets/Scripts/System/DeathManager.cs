using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathManager : MonoBehaviour
{
    private int _enemyCount = 0;
    private LockOn _lockOn = default;
    private SearchNearEnemy _nearEnemy = default;

    private bool _canJudge = true;
    private void Start()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyCount = enemys.Length;
        GameObject plObj = GameObject.FindWithTag("Player");
        _lockOn = plObj.GetComponent<LockOn>();
        Debug.Log("ìGÇÃêîÇÕ" + _enemyCount);
        StartCoroutine(GetNearEnemy(plObj));
    }

    private IEnumerator GetNearEnemy(GameObject plObj)
    {
        yield return new WaitForSeconds(0.1f);
        _nearEnemy = plObj.GetComponent<SearchNearEnemy>();
    }
    public void PlayerCheckHP(int hp)
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene("DeathResult");
        }
    }

    public void EnemyCheckHP(int hp)
    {
        if (!_canJudge)
        {
            return;
        }
        if(hp <= 0)
        {
            _canJudge = false;
            _enemyCount--;
            StartCoroutine(ReturnTimeScale());
            Debug.Log("écÇËÇÃìGÇÃêî" + _enemyCount);
            if(_enemyCount <= 0)
            {
                SceneManager.LoadScene("WinResult");
            }
        }
    }

    private IEnumerator ReturnTimeScale()
    {
        _nearEnemy.ChangeEnemyArray();
        _lockOn.ChangeCamera();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        _canJudge = true;
    }
}
