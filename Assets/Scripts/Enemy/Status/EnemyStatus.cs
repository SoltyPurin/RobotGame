using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable Objects/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField, Header("敵のタイプ")]
    private EnemyType _enemyType = EnemyType.Normal;
    public EnemyType EnemyType { get { return _enemyType; } }
    [SerializeField, Header("どれくらい弾が近づいたら回避するか")]
    private float _dodgeRange = 5f;
    public float DodgeRange {  get { return _dodgeRange; }}
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    public float MoveMaxDistance {  get { return _moveMaxDistance; }}
    [SerializeField, Header("突進時の速度")]
    private float _rushSpeed = 40f;
    public float RushSpeed {  get { return _rushSpeed; }}
    [SerializeField, Header("突進時間")]
    private float _rushTime = 1.4f;
    public float RushTime { get { return _rushTime; }}
    [SerializeField, Header("近接で与えるダメージ")]
    private int _meleeDamageValue = 50;
    public int MeleeDamageValue { get { return _meleeDamageValue; }}
    [SerializeField, Header("近接攻撃の吹き飛ばし力")]
    private float _meleeBlowAwayPower = 50f;
    public float MeleeBlowAwayPower { get { return _meleeBlowAwayPower; }}
    [SerializeField, Header("射撃で与えるダメージ")]
    private int _bulletDamageValue = 50;
    public int BulletDamageValue { get {return _bulletDamageValue; }}
    [SerializeField, Header("射撃での吹き飛ばし力")]
    private float _bulletBlowAwayPower = 50;
    public float BulletBlowAwayPower { get {return _bulletBlowAwayPower; }}
    [SerializeField, Header("銃弾の生存時間")]
    private float _bulletAliveTime = 5;
    public float BulletAliveTime { get {return _bulletAliveTime; }}
    [SerializeField, Header("重力")]
    private float _gravity = 150;
    public float Gravity { get { return _gravity; }}
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;
    public float NearTargetPosDistance { get { return _nearTargetPosDistance; }}
    [SerializeField, Header("移動速度")]
    private float _aiMoveSpeed = 50f;
    public float AiMoveSpeed { get { return _aiMoveSpeed; }}
    [SerializeField, Header("回避時に与える力")]
    private float _dodgePower = 10;
    public float DodgePower { get { return _dodgePower; }}
    [SerializeField, Header("回避時間")]
    private float _dodgeTime = 0.5f;
    public float DodgeTime { get { return _dodgeTime; }}
    [SerializeField, Header("ジャンプ力")]
    private float _jumpPower = 10;
    public float JumpPower { get { return _jumpPower; }}
    [SerializeField, Header("待機状態の待機時間")]
    private float _stopTime = 1;
    public float StopTime { get { return _stopTime; }}
    [SerializeField, Header("何回移動したら飛ぶようにするか")]
    private int _moveCountUntilJump = 3;
    public int MoveCountUntilJump { get { return _moveCountUntilJump; }}
    [SerializeField, Header("何秒先読みするか")]
    private float _sakiyomiTime = 1;
    public float SakiyomiTime { get {return _sakiyomiTime; }}
    [SerializeField, Header("速度がどれくらい遅かったら先読みをやめるか")]
    private float _sakiyomiStopSpeed = 10;
    public float SakiyomiStopSpeed { get { return _sakiyomiStopSpeed; }}
    [SerializeField, Header("近接に発展する距離")]
    private float _meleeAttackRange = 50;
    public float MeleeAttackRange { get { return _meleeAttackRange; }}

}
