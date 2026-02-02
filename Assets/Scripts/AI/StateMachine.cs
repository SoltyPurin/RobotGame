using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IEnemyState _currentState; // 現在アクティブな状態を保持するフィールド
    private IEnemyState _prevState;

    public void ChangeState(IEnemyState newState,in TestAIController controller,in EnemyContext ctx)
    {
        _currentState = newState;  // 新しい状態を現在の状態に設定
        _prevState = newState;
        _currentState.Enter(controller,ctx);  // 新しい状態の初期化処理を実行
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();  // 現在の状態のUpdateメソッドを呼び出す
    }
}
