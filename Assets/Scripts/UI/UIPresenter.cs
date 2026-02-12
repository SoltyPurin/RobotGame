using UnityEngine;
using UniRx;
using System;

public class UIPresenter : MonoBehaviour
{
    private GameObject _player = default;
    private PlayerInputManager _input = default;
    private PlayerMove _move = default;
    private AuraBurst _burst = default;
    private UIViewer _viewer = default;

    private CompositeDisposable _inputDispose = new CompositeDisposable();
    private CompositeDisposable _moveDispose = new CompositeDisposable();



    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        _input = _player.GetComponent<PlayerInputManager>();
        _move = _player.GetComponent<PlayerMove>();
        _burst = _player.GetComponent<AuraBurst>();
        _viewer = GetComponent<UIViewer>();

        _viewer.SetDashTimeSliderMax(_move.DashTimeProperty.Value);
        _viewer.SetShotWeaponMax(_input.ShootCoolTimeProperty.Value);

        _move.DashTimeProperty.Subscribe(dash =>
        {
            _viewer.SetDashValue(dash);
        });

        _move.IsRunning.Subscribe(isRunning =>
        {
            _viewer.SwitchDashEffect(isRunning);
        });

        _input.ShootCoolTimeProperty.Subscribe(coolTime =>
        {
            _viewer.SetShotWeaponValue(coolTime);
        });

        _burst.CanUseBurst.Subscribe(canUseBurst =>
        {
            _viewer.OnlineGoMark(canUseBurst);
        });
    }

    private void OnDisable()
    {
        Debug.Log("çwì«èIóπ");
        _inputDispose.Dispose();
        _inputDispose.Clear();
        _moveDispose.Dispose();
        _moveDispose.Clear();
    }

}
