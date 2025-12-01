using UnityEngine;

public class AIJump : Jump
{

    [SerializeField,Header("コントローラー")]
    private AIControllScript _controller = default;
    public override void JumpProtocol()
    {
        base.JumpProtocol();
        _controller.ThinkNextMove();
    }
}
