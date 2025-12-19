using UnityEngine;
using UniRx;
public class UIPresenter : MonoBehaviour
{
    private GameObject _player;
    private TakeDamageScript _takeDamage;
    private UIViewer _viewr;
    [SerializeField, Header("死亡管理のスクリプト")]
    private DeathManager _deathManager = default;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _takeDamage = _player.GetComponent<TakeDamageScript>();
        _viewr = GetComponent<UIViewer>();
        _takeDamage.Health.Subscribe(x =>
            {
                // Viewに反映
                _viewr.SetHealth(x);
                _deathManager.CheckHP(x);
            }).AddTo(this);
    }
}
