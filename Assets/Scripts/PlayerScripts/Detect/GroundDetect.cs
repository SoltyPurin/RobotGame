using System.Linq.Expressions;
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
    public float RayDistance {  get { return _rayDistance; } }
    [SerializeField, Header("アニメーター")]
    private Animator _animator = default;

    private bool _wasGrounded = false;

    private readonly string GROUND_TAG = "Ground";

    //private void OnCollisionEnter(Collision collision)
    //{
    //    GameObject obj = collision.gameObject;
    //    if (obj.CompareTag(GROUND_TAG))
    //    {
    //        _animator.SetBool("IsTouchGround", true);
    //        _effect.StopThrusterEffect();
    //        _jump.CanJumpSwitch();
    //        if (!_move.IsRunning.Value)
    //        {
    //            _move.DashTimeHeal();
    //        }
    //        _move.Landing();
    //    }
    //}

    public void TryHealDashGage()
    {
        if (IsGround())
        {
            _move.DashTimeHeal();
        }
    }

    public bool IsGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, _rayDistance);
    }
    private void Update()
    {
        // 毎フレーム判定を更新（アニメーターに反映）
        bool isGrounded = IsGround();
        _animator.SetBool("IsTouchGround", isGrounded);

        if (isGrounded && !_wasGrounded)
        {
            OnJustLanded();
            TryHealDashGage();
        }

        if (isGrounded)
        {
            _jump.CanJumpSwitch();
        }

        _wasGrounded = isGrounded;
        if (isGrounded)
        {
            _jump.CanJumpSwitch();
        }
    }

    private void OnJustLanded()
    {
        _effect.StopThrusterEffect();
        _jump.CanJumpSwitch();

        if (!_move.IsRunning.Value)
        {
            _move.DashTimeHeal();
        }
        _move.Landing();
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    GameObject obj = collision.gameObject;
    //    if (obj.CompareTag(GROUND_TAG))
    //    {
    //        _animator.SetBool("IsTouchGround",false);
    //    }
    //    }

    //public float GroundYAxis(float x, float z)
    //{
    //    RaycastHit hit;
    //    Vector3 startPos = new Vector3(x, transform.position.y, z);
    //    Physics.Raycast(startPos, Vector3.down, out hit, _rayDistance);
    //    return hit.point.y;
    //}

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * _rayDistance, Color.red);
    }
}
