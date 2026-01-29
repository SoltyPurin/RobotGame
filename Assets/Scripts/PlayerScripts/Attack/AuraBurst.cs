using System.Collections;
using UniRx;
using UnityEngine;

public class AuraBurst : MonoBehaviour
{
    //攻撃バーストは攻撃力
    //スピードバーストは速度上昇
    //防御バーストは一定時間無敵
    [SerializeField, Header("攻撃力上昇の時間")]
    private float _attackBurstTime = 9.5f;
    [SerializeField, Header("バースト中に上げる攻撃力(近接遠距離共通)")]
    private int _burstUpAttackValue = 100;
    [SerializeField, Header("スピード上昇の時間")]
    private float _speedBurstTime = 15;
    [SerializeField, Header("バースト中に上げる速度")]
    private int _burstSpeed = 100;
    [SerializeField, Header("防御バーストの無敵時間")]
    private float _invincibleTime = 5;

    private int _burstTypeIndex = 0;
    private readonly string BURST_TYPE = "BurstType";

    private PlayerMove _playerMove = default;
    private RightAttack _rightAttack = default;
    private LeftAttack _leftAttack = default;
    private PlayerTakeDamage _takeDamage = default;
    private PlayerSoundPlayScript _soundPlay = default;
    private AuraBurstEffectPlay _effect = default;
    private AuraBurstPerformance _performance = default;

    private int _useableBurstCount = 1;　　
    private ReactiveProperty<bool> _canUseBurst = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> CanUseBurst
    {
        get { return _canUseBurst; }
    }
    private void Start()
    {
        _burstTypeIndex = PlayerPrefs.GetInt(BURST_TYPE, 0);
        _playerMove = GetComponent<PlayerMove>();
        _effect = GetComponent<AuraBurstEffectPlay>();
        _rightAttack = FindAnyObjectByType<RightAttack>();  
        _leftAttack = FindAnyObjectByType<LeftAttack>();
        _takeDamage = GetComponent<PlayerTakeDamage>();
        _performance = FindAnyObjectByType<AuraBurstPerformance>();
        _soundPlay = FindAnyObjectByType<PlayerSoundPlayScript>();
    }
    public void StartAuraBurst()
    {
        if( !_canUseBurst.Value)
        {
            return;
        }
        if(_useableBurstCount <= 0)
        {
            return;
        }
        _useableBurstCount--;
        _performance.AuraBurstCutIn(this.gameObject);
        switch (_burstTypeIndex)
        {
            case (int)BurstName.Attack:
                Debug.Log("攻撃力上昇");
                _rightAttack.AuraBurst(_burstUpAttackValue,true);
                _leftAttack.AuraBurst(_burstUpAttackValue, true);
                StartCoroutine(UnLockBurst(_attackBurstTime));
                break;

            case (int)BurstName.Speed:
                Debug.Log("スピード上昇");
                _playerMove.AuraBurst(_burstSpeed, true);
                StartCoroutine(UnLockBurst(_speedBurstTime));
                break;

            case (int)BurstName.Guard:
                Debug.Log("無敵化");
                _takeDamage.AuraBurst(true);
                StartCoroutine(UnLockBurst(_invincibleTime));
                break;
        }
        
        _effect.SetEffect(_burstTypeIndex,true);
    }

    private IEnumerator UnLockBurst(float unlockTime)
    {
        yield return new WaitForSeconds(unlockTime);

        Debug.Log("解除");
        switch (_burstTypeIndex)
        {
            case (int)BurstName.Attack:
                _rightAttack.AuraBurst(_burstUpAttackValue, false);
                _leftAttack.AuraBurst(_burstUpAttackValue, false);
                break;

            case (int)BurstName.Speed:
                _playerMove.AuraBurst(_burstSpeed, false);
                break;

            case (int)BurstName.Guard:
                _takeDamage.AuraBurst(false);
                break;
        }
        _effect.SetEffect(_burstTypeIndex, false);

        _canUseBurst.Value = false;
    }

    public void AuraBurstUseableProtocol()
    {
        _canUseBurst.Value = true;
        _soundPlay.PlayBurstReadySound();
    }
}
