using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    [SerializeField, Header("ジャンプのスクリプト")]
    private Jump _jump = default;
    [SerializeField, Header("移動のスクリプト")]
    private PlayerMove _move = default;
    [SerializeField, Header("エフェクトプレイヤー")]
    private PlayerEffectPlay _effect = default;
    [SerializeField, Header("地面に照射するレイの長さ")]
    private float _rayDistance = 0.5f;

    private readonly string GROUND_TAG = "Ground";

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag(GROUND_TAG))
        {
            _effect.StopThrusterEffect();
            _jump.JumpCountReset();
            if (!_move.IsRunning)
            {
                _move.DashTimeHeal();
            }
        }
    }

    public float GroundYAxis(float x, float z)
    {
        RaycastHit hit;
        Vector3 startPos = new Vector3(x, transform.position.y, z);
        Physics.Raycast(startPos, Vector3.down, out hit, _rayDistance);
        return hit.point.y;
    }
}
