using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    [SerializeField, Header("ジャンプのスクリプト")]
    private Jump _jump = default;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Ground"))
        {
            _jump.JumpCountReset();
        }
    }
}
