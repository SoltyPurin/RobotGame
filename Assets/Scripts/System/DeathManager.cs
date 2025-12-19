using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public void PlayerCheckHP(int hp)
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene("DeathResult");
        }
    }

    public void EnemyCheckHP(int hp)
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene("WinResult");
        }
    }
}
