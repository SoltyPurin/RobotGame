using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IEnemyState _currentState; // 現在アクティブな状態を保持するフィールド
    private IEnemyState _prevState;

    public void ChangeState(IEnemyState newState,in TestAIController controller,in EnemyContext ctx)
    {
        Debug.Log("前のステート"+_prevState);
        _currentState?.Exit();  // 現在の状態が存在する場合、終了処理を呼び出す
        _currentState = newState;  // 新しい状態を現在の状態に設定
        Debug.Log("現在のステート" + newState);
        _prevState = newState;
        _currentState.Enter(controller,ctx);  // 新しい状態の初期化処理を実行
    }

    public void FixedUpdate()
    {
        _currentState?.Update();  // 現在の状態のUpdateメソッドを呼び出す
    }
}
