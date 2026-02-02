using System.Collections;
using UnityEngine;

public class CanvasOn : MonoBehaviour
{
    [SerializeField, Header("何秒後にキャンバスを表示するか")]
    private float _canvasViewTime = 1;

    private CanvasGroup _group = default;

    private void Start()
    {
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0;
        _group.interactable = false;
        _group.blocksRaycasts = false;
        StartCoroutine(CanvasOnline());
    }

    private IEnumerator CanvasOnline()
    {
        yield return new WaitForSeconds(_canvasViewTime);
        _group.alpha = 1;
        _group.interactable = true;
        _group.blocksRaycasts = true;
    }
}
