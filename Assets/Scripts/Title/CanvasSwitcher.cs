using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    private Stack<CanvasGroup> _canvasStuck = new Stack<CanvasGroup> ();
    private CanvasGroup _currentcanvasGroup;
    [SerializeField, Header("戻るボタンがあるキャンバス")]
    private CanvasGroup _returnButtonCanvas = default;
    [SerializeField,Header("タイトルのキャンバス")]
    private CanvasGroup _titleCanvas = default;
    [SerializeField,Header("全てのキャンバスグループのリスト")]
    private List<CanvasGroup> _allCanvasGroup = new List<CanvasGroup> ();

    private void Start()
    {
        ResetToTitle();
    }
    public void StuckIn(GameObject curCanvas,GameObject nextCanvas)
    {
        CanvasGroup canvas = curCanvas.GetComponent<CanvasGroup>();
        _returnButtonCanvas.alpha = 1;
        _returnButtonCanvas.interactable = true;
        CanvasGroup current = curCanvas.GetComponent<CanvasGroup>();
        CanvasGroup next = nextCanvas.GetComponent<CanvasGroup>();
        if (current == null || next == null)
        {
            return;
        }
        next.alpha = 1;
        next.interactable = true;
        next.blocksRaycasts = true;
        _currentcanvasGroup = next;
        current.alpha = 0;
        current.interactable = false;
        current.blocksRaycasts = false;
        _canvasStuck.Push(current);
    }

    public void StuckOut()
    {
        _currentcanvasGroup.interactable = false;
        _currentcanvasGroup.alpha = 0;
        _currentcanvasGroup.blocksRaycasts= false;
        if( _canvasStuck.Count > 1)
        {
            CanvasGroup popCanvas = _canvasStuck.Pop();
            popCanvas.alpha = 1;
            popCanvas.interactable = true;
            popCanvas.blocksRaycasts = true;
            _currentcanvasGroup = popCanvas;
        }
        else
        {
            CanvasGroup popCanvas = _canvasStuck.Pop();
            popCanvas.alpha = 1;
            popCanvas.interactable = true;
            popCanvas.blocksRaycasts = true;
            _currentcanvasGroup = popCanvas;
            _returnButtonCanvas.interactable= false;
            _returnButtonCanvas.alpha = 0;
        }
    }

    private void ResetToTitle()
    {
        for(int i = 0; i <_allCanvasGroup.Count; i++)
        {
            _allCanvasGroup[i].blocksRaycasts = false;
            _allCanvasGroup[i].interactable = false;
            _allCanvasGroup[i].alpha = 0;
        }
        _returnButtonCanvas.blocksRaycasts = true;
        _currentcanvasGroup = _titleCanvas;
        _titleCanvas.blocksRaycasts = true;
        _titleCanvas.interactable = true;
        _titleCanvas.alpha = 1;

    }

    private void OnEnable()
    {
        _currentcanvasGroup = _titleCanvas;
    }
}
