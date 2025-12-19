using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public void CheckHP(int hp)
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene("DeathResult");
        }
    }
}
