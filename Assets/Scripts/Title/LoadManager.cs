using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{

    // ロードするシーンの名前
    [SerializeField,Header("次に読み込む本編のシーン")]
    private string _nextSceneName;

    // ロードの進捗状況を表示するUIなど
    [SerializeField,Header("NowLoadingのオブジェクト")]
    public GameObject loadingUI;

    // ロードの進捗状況を管理するための変数
    private AsyncOperation async;

    // ロードを開始するメソッド
    public void StartLoad()
    {
        StartCoroutine(Load());
    }

    // コルーチンを使用してロードを実行するメソッド
    private IEnumerator Load()
    {
        // ロード画面を表示する
        loadingUI.SetActive(true);

        // シーンを非同期でロードする
        async = SceneManager.LoadSceneAsync(_nextSceneName);

        // ロードが完了するまで待機する
        while (!async.isDone)
        {
            yield return null;
        }

        // ロード画面を非表示にする
        loadingUI.SetActive(false);
    }
}
