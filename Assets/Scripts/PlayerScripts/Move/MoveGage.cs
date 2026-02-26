using UniRx;
using UnityEngine;

public class MoveGage : MonoBehaviour
{
    [SerializeField,Header("ダッシュやジャンプできる時間")]
    private FloatReactiveProperty _moveValue;

    public IReadOnlyReactiveProperty<float> MoveTimeProperty
    {
        get { return _moveValue; }
    }

    private float _saveDashTime;

    private void Start()
    {
        _saveDashTime = _moveValue.Value;
    }

    /// <summary>
    /// ダッシュ及びジャンプ中に呼び出す。必ずFixedUpdateで呼び出すこと
    /// </summary>
    public void Moveing()
    {
        if (_moveValue.Value >= 0)
        {
            _moveValue.Value -= Time.fixedDeltaTime;
        }
    }

    public void ResetMoveValue()
    {
        _moveValue.Value = _saveDashTime;
    }
}
