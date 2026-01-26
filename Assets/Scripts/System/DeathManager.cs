using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class DeathManager : MonoBehaviour
{
    [SerializeField, Header("ロックオンのカメラ")]
    private CinemachineThirdPersonFollow _camera = default;
    private int _enemyCount = 0;
    private LockOn _lockOn = default;
    private SearchNearEnemy _nearEnemy = default;

    private bool _canJudge = true;
    private void Start()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyCount = enemys.Length;
        _lockOn = FindAnyObjectByType<LockOn>();
        _nearEnemy = FindAnyObjectByType<SearchNearEnemy>();
    }

    public void PlayerCheckHP(int hp)
    {
        StartCoroutine(PlayerDeathDelay());
    }

    private IEnumerator PlayerDeathDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("DeathResult");
    }

    public void EnemyCheckHP(int hp)
    {
        if (!_canJudge)
        {
            return;
        }
        if(hp <= 0)
        {
            Time.timeScale = 0.2f;
            _canJudge = false;
            _enemyCount--;
            _lockOn.ReSearch();
            StartCoroutine(ReturnTimeScale());
            Debug.Log("残りの敵の数" + _enemyCount);

        }
    }

    private IEnumerator ReturnTimeScale()
    {
        if(_enemyCount <= 0)
        {
           _camera.ShoulderOffset.z = 165;
           _lockOn.UnlockTarget();
        }
        else
        {
            _nearEnemy.ChangeEnemyArray();
            _lockOn.ChangeCamera();
        }
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        _canJudge = true;
        if (_enemyCount <= 0)
        {
            SceneManager.LoadScene("WinResult");
        }

    }
}
