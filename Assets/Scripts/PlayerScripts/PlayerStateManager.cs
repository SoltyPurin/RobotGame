using PlayerState;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private e_PlayerState _currentState = e_PlayerState.Normal;
    public e_PlayerState CurrentPlayerState {
        get { return _currentState; } 
    }

    public void ChangeDodgeState()
    {
        if(_currentState == e_PlayerState.LeftAttack)
        {
            return;
        }

        _currentState = e_PlayerState.Dodge;
    }

    public void ChangeLeftAttackState()
    {
        _currentState = e_PlayerState.LeftAttack;
    }

    public void ChangeNormalState()
    {
        _currentState = e_PlayerState.Normal;
    }
}
