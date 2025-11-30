using UnityEngine;

public class AIMove : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;

    public void MoveProtocol(Vector3 targetPos,float moveDistance)
    {

    }
}
