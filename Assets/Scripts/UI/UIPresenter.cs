using UnityEngine;
using UniRx;
using System;

public class UIPresenter : MonoBehaviour
{
    private GameObject _player = default;
    private PlayerInputManager _input = default;
    private PlayerMove _move = default;
    private UIViewer _viewer = default;

    private CompositeDisposable _inputDispose = new CompositeDisposable();
    private CompositeDisposable _moveDispose = new CompositeDisposable();



    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        _input = _player.GetComponent<PlayerInputManager>();
        _move = _player.GetComponent<PlayerMove>();
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
    }

    private void OnDisable()
    {
        Debug.Log("çwì«èIóπ");
        _inputDispose.Dispose();
        _moveDispose.Dispose();
    }

}
