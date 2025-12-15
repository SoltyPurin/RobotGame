using UnityEngine;
using UniRx;
public class UIPresenter : MonoBehaviour
{
    private GameObject _player;
    private TakeDamageScript _takeDamage;
    private UIViewer _viewr;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _takeDamage = _player.GetComponent<TakeDamageScript>();
        _viewr = GetComponent<UIViewer>();
        _takeDamage.Health.Subscribe(x =>
            {
                // View‚É”½‰f
                _viewr.SetHealth(x);
            }).AddTo(this);
    }
}
