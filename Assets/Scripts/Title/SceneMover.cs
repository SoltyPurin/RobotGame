using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMover : MonoBehaviour
{
    [SerializeField, Header("ゲームスタートボタン")]
    private Button _gameStart = default;
    [SerializeField,Header("終了ボタン")]
    private Button _exitButton = default;
    private void Start()
    {
        _gameStart.onClick.AddListener(GameStart);
        _exitButton.onClick.AddListener(GameExit);
    }

    private void GameStart()
    {
        SceneManager.LoadScene("Honpen");
    }

    private void GameExit()
    {
        Application.Quit();
    }
}
