using UnityEngine;

public class AuraBurstEffectPlay : MonoBehaviour
{
    [SerializeField, Header("アタックのエフェクト")]
    private GameObject _attackEffect = default;
    [SerializeField, Header("スピードのエフェクト")]
    private GameObject _speedEffect = default;
    [SerializeField, Header("防御のエフェクト")]
    private GameObject _guardEffect = default;

    public void SetEffect(int index,bool isTrue)
    {
        switch (index)
        {
            case (int)BurstName.Attack:
                _attackEffect.SetActive(isTrue);
                break;

            case (int)BurstName.Speed:
                _speedEffect.SetActive(isTrue);
                break;

            case (int)BurstName.Guard:
                _guardEffect.SetActive(isTrue);
                break;
        }
    }
}
