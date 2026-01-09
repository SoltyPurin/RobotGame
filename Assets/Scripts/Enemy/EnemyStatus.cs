using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable Objects/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField, Header("どれくらい弾が近づいたら回避するか")]
    private float _dodgeRange = 5f;
    [SerializeField, Header("その場所から移動する最大距離")]
    private float _moveMaxDistance = 10;
    [SerializeField, Header("突進時の速度")]
    private float _rushSpeed = 40f;
    [SerializeField, Header("突進時間")]
    private float _rushTime = 1.4f;
    [SerializeField, Header("近接で与えるダメージ")]
    private int _meleeDamageValue = 50;
    [SerializeField, Header("近接攻撃の吹き飛ばし力")]
    private float _meleeBlowAwayPower = 50f;
    [SerializeField, Header("射撃で与えるダメージ")]
    private int _bulletDamageValue = 50;
    [SerializeField, Header("射撃での吹き飛ばし力")]
    private float _bulletBlowAwayPower = 50;
    [SerializeField, Header("銃弾の生存時間")]
    private float _bulletAliveTime = 5;
    [SerializeField, Header("重力")]
    private float _gravity = 150;
    [SerializeField, Header("射撃開始地点")]
    private Transform _shootPoint = default;
    [SerializeField, Header("近接攻撃の判定の大きさ")]
    private Vector3 _meleeRangeSize;
    [SerializeField, Header("近接判定の中心座標")]
    private Vector3 _meleeRangeCenter;
    [SerializeField, Header("どれくらいターゲットの座標に近づいたら到着判定になるか")]
    private float _nearTargetPosDistance = 5;
    [SerializeField, Header("移動速度")]
    private float _aiMoveSpeed = 50f;
    [SerializeField, Header("回避時に与える力")]
    private float _dodgePower = 10;
    [SerializeField, Header("回避時間")]
    private float _dodgeTime = 0.5f;
    [SerializeField, Header("ジャンプ力")]
    private float _jumpPower = 10;
    [SerializeField, Header("待機状態の待機時間")]
    private float _stopTime = 1;
    [SerializeField, Header("何回移動したら飛ぶようにするか")]
    private int _moveCountUntilJump = 3;
    [SerializeField, Header("何秒先読みするか")]
    private float _sakiyomiTime = 1;
    [SerializeField, Header("速度がどれくらい遅かったら先読みをやめるか")]
    private float _sakiyomiStopSpeed = 10;
    [SerializeField, Header("近接に発展する距離")]
    private float _meleeAttackRange = 50;


}
