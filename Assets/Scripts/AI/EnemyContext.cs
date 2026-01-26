using UnityEngine;

public class EnemyContext
{
    public Transform Transform;
    public Transform PlayerTransform;
    public Vector3 PlayerPosition;
    public Transform ShootPoint;
    public Rigidbody OnBallRigidbody;
    public Rigidbody BallRigidBody;
    public TestAIController Controller;
    public EnemyDetectGround Ground;
    public PlayAnimationScript Animation;
    public BulletPool Pool;
    public EnemySoundPlayScript SoundPlayScript;
    public float MoveSpeed;
    public float EscapeSpeed;
    public float DodgePower;
    public float DodgeTime;
    public float JumpPower;
    public float StopTime;
    public float RushSpeed;
    public float RushTime;
    public float MeleeBlowAwayPower;
    public int MeleeDamage;
    public float BulletBlowAwayPower;
    public int BulletDamage;
    public float BulletAliveTime;
    public float BulletMoveSpeed;
    public float Gravity;
    public float Atosuki;
    public Vector3 MeleeRangeSize;
    public Vector3 MeleeRangeCenter;
    // ÇŸÇ©ã§í Ç≈éùÇ¡ÇƒÇ®Ç´ÇΩÇ¢Ç‡ÇÃ
}
