using UnityEngine;
using UnityEngine.EventSystems;
public class GetTitleButtonMove : MonoBehaviour
{
    private GameObject _currentSelectButton = default;
    private GameObject _prevSelectButton = default;
    private TitleSoundManager _soundManager = default;
    private void Start()
    {
        _soundManager = GetComponent<TitleSoundManager>();
        _currentSelectButton = EventSystem.current.currentSelectedGameObject;
        _prevSelectButton = EventSystem.current.currentSelectedGameObject;
    }
    private void Update()
    {
        _currentSelectButton = EventSystem.current.currentSelectedGameObject;
        if(_currentSelectButton != _prevSelectButton)
        {
            _soundManager.PlayButtonMoveSE();
            _prevSelectButton = _currentSelectButton;
        }
    }
}
