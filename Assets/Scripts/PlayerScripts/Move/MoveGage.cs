using UniRx;
using UnityEngine;

public class MoveGage : MonoBehaviour
{
    //ジャンプは3秒、ダッシュは5秒できる
    [SerializeField,Header("ダッシュやジャンプできる時間")]
    private FloatReactiveProperty _moveValue;

    public IReadOnlyReactiveProperty<float> MoveTimeProperty
    {
        get { return _moveValue; }
    }
    [SerializeField, Header("ジャンプの時に時間を減らす時の倍率")]
    private float _jumpValueMultiplier = 1.1f;
    private float _saveDashTime;

    private void Start()
    {
        _saveDashTime = _moveValue.Value;
    }

    /// <summary>
    /// ダッシュ及びジャンプ中に呼び出す。必ずFixedUpdateで呼び出すこと
    /// </summary>
    public void Moveing(bool isJump)
    {
        if(_moveValue.Value < 0)
        {
            return;
        }
        switch (isJump)
        {
            case true:
                _moveValue.Value -= Time.fixedDeltaTime * _jumpValueMultiplier;
                break;

            case false:
                    _moveValue.Value -= Time.fixedDeltaTime;
                break;  
        }
    }

    public void ResetMoveValue()
    {
        _moveValue.Value = _saveDashTime;
    }
}
