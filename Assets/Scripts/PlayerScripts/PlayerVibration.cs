using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerVibration : MonoBehaviour
{
    private Gamepad _gamePad = default;
    private void Awake()
    {
        _gamePad = Gamepad.current;
    }
    public IEnumerator MeleeDamageVibe(float left, float right)
    {
        if(_gamePad == null)
        {
            yield break;
        }
        _gamePad.SetMotorSpeeds(left, right);
        yield return new WaitForSeconds(1.0f);
        _gamePad.SetMotorSpeeds(0, 0);
    }

    public IEnumerator ShootDamageVibe(float left, float right)
    {
        if (_gamePad == null)
        {
            yield break;
        }
        _gamePad.SetMotorSpeeds(left, right);
        yield return new WaitForSeconds(1.0f);
        _gamePad.SetMotorSpeeds(0, 0);
    }
}
