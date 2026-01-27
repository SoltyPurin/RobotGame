using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class DeathManager : MonoBehaviour
{
    [SerializeField, Header("ロックオンのカメラ")]
    private GameObject _camera = default;
    private CinemachineCamera _currentCamera = default;
    [SerializeField, Header("死亡時のカメラを管理するスクリプト")]
    private DeathCamera _deathCamera = default;
    private int _enemyCount = 0;
    private LockOn _lockOn = default;
    private SearchNearEnemy _nearEnemy = default;

    private bool _canJudge = true;
    private void Start()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _currentCamera = _camera.GetComponent<CinemachineCamera>();
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

    public void EnemyCheckHP(int hp,GameObject enemy)
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
            StartCoroutine(ReturnTimeScale(enemy));
            Debug.Log("残りの敵の数" + _enemyCount);

        }
    }

    private IEnumerator ReturnTimeScale(GameObject enemy)
    {
        if(_enemyCount <= 0)
        {
            Debug.Log("敵が全滅");
            _deathCamera.MoveDeathObject(enemy, _currentCamera);
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
