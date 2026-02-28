using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField, Header("説明文を書くテキスト")]
    private TextMeshPro _tutorialText = default;
    [SerializeField,Header("チュートリアル説明文")]
    private List<string> _sentences = new List<string>();

    int _sentencesIndex = 0;

    private void Awake()
    {
        _tutorialText.text = _sentences[_sentencesIndex];
    }

    public void NextTutorial()
    {
        _sentencesIndex++;
        _tutorialText.text = _sentences[_sentencesIndex];
    }
}
