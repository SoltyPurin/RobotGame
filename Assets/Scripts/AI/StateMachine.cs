using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IEnemyState currentState; // 現在アクティブな状態を保持するフィールド

    public void ChangeState(IEnemyState newState,in TestAIController controller,in EnemyContext ctx)
    {
        currentState?.Exit();  // 現在の状態が存在する場合、終了処理を呼び出す
        currentState = newState;  // 新しい状態を現在の状態に設定
        currentState.Enter(controller,ctx);  // 新しい状態の初期化処理を実行
    }

    public void Update()
    {
        currentState?.Update();  // 現在の状態のUpdateメソッドを呼び出す
    }
}
