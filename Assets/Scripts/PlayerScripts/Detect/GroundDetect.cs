using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    [SerializeField, Header("ジャンプのスクリプト")]
    private Jump _jump = default;
    [SerializeField, Header("エフェクトプレイヤー")]
    private PlayerEffectPlay _effect = default;

    private readonly string GROUND_TAG = "Ground";

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag(GROUND_TAG))
        {
            _effect.StopThrusterEffect();
            _jump.JumpCountReset();
        }
    }
}
