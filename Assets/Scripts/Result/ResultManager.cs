using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField, Header("リトライボタン")]
    private Button _retryButton = default;
    [SerializeField, Header("タイトルに戻るボタン")]
    private Button _returnTitleButton = default;

    private void Start()
    {
        _retryButton.onClick.AddListener(Retry);
        _returnTitleButton.onClick.AddListener(ReturnTitle);
    }

    private void Retry()
    {
        SceneManager.LoadScene("Honpen");
    }

    private void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
