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
    private MoveGage _moveGage = default;

    private CompositeDisposable _inputDispose = new CompositeDisposable();



    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        _input = _player.GetComponent<PlayerInputManager>();
        _move = _player.GetComponent<PlayerMove>();
        _moveGage = _player.GetComponent<MoveGage>();
        _burst = _player.GetComponent<AuraBurst>();
        _viewer = GetComponent<UIViewer>();

        _viewer.SetDashTimeSliderMax(_moveGage.MoveTimeProperty.Value);
        _viewer.SetShotWeaponMax(_input.ShootCoolTimeProperty.Value);

        IDisposable dashSubscibe = _moveGage.MoveTimeProperty.Subscribe(dash =>
        {
            _viewer.SetDashValue(dash);
        });


        IDisposable isRunningSubscribe= _move.IsRunning.Subscribe(isRunning =>
        {
            _viewer.SwitchDashEffect(isRunning);
        });

        IDisposable shootCoolTimeSubscribe =   _input.ShootCoolTimeProperty.Subscribe(coolTime =>
        {
            _viewer.SetShotWeaponValue(coolTime);
        });

        IDisposable canUseBurstSubscribe = _burst.CanUseBurst.Subscribe(canUseBurst =>
        {
            _viewer.OnlineGoMark(canUseBurst);
        });

        _inputDispose.Add(dashSubscibe);
        _inputDispose.Add(isRunningSubscribe);
        _inputDispose.Add(shootCoolTimeSubscribe);
        _inputDispose.Add(canUseBurstSubscribe);
    }

    private void OnDisable()
    {
        Debug.Log("çwì«èIóπ");
        _inputDispose.Dispose();
    }

}
