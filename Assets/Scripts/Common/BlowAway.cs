using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class BlowAway : MonoBehaviour
{
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("ヒットストップさせるフレーム数")]
    private float _stopFlame = 30;
    public void BlowAwayProtocol(Vector3 direction,float blowAwayPower)
    {
        HitStop(direction, blowAwayPower);
    }

    private async UniTask HitStop(Vector3 direction, float blowAwayPower)
    {
        for (int i = 0; i < _stopFlame; i++)
        {
            await UniTask.DelayFrame(1); 
        }
        Debug.Log("吹き飛んだ");

        _onBallRigidBody.AddForce(direction * blowAwayPower, ForceMode.Impulse);
    }
}
