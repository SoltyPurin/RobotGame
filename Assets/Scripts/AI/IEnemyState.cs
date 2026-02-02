public interface IEnemyState
{
    /// <summary>
    /// ‚»‚Ìó‘Ô‚É“ü‚é‚½‚ß‚Ì‚Æ‚«‚Ìˆ—
    /// </summary>
    void Enter(in TestAIController controller, in EnemyContext ctx);  
    /// <summary>
    /// –ˆƒtƒŒ[ƒ€‚Ìˆ—
    /// </summary>
    void FixedUpdate(); 
}