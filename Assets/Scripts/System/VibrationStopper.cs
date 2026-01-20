using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationStopper : MonoBehaviour
{
    private Gamepad _gamePad = default;
    private void Awake()
    {
        _gamePad = Gamepad.current;
        if(_gamePad != null)
        {
            _gamePad.SetMotorSpeeds(0, 0);
        }
    }
}
