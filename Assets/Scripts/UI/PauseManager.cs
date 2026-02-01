using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField, Header("ボタン")]
    private Button _resumeButton = default;
    [SerializeField]
    private Button _exitButton = default;
    private bool _isPauseing = false;
    private UIViewer _ui = default;

    private void Start()
    {
        Time.timeScale = 1;
        _ui = GetComponent<UIViewer>();
        _resumeButton.onClick.AddListener(Resume);
        _exitButton.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        InputPause();
    }

    private void Exit()
    {
        SceneManager.LoadScene("Title");
    }
    public void InputPause()
    {
        if (_isPauseing)
        {
            //ポーズ解除
            _ui.PauseBreak();
            Time.timeScale = 1;
            _isPauseing = false;
        }
        else
        {
            //ポーズ開始
            _ui.Pause();
            Time.timeScale = 0;
            _isPauseing=true;
        }
    }
}
