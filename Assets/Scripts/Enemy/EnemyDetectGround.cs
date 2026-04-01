using UnityEngine;

public class EnemyDetectGround : MonoBehaviour
{
    [SerializeField, Header("アニメーター")]
    private Animator _animator = default;
    [SerializeField, Header("アニメ再生")]
    private PlayAnimationScript _animScript = default;
    private float _rayDistance = 5.5f;
    private bool _isTouchTheGround = false;
    public bool IsTouchTheGround
    {
        get { return _isTouchTheGround;}
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Ground"))
        {
            _isTouchTheGround = true;
        }
        _animScript.LandingAnim();
    }
    public bool IsGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, _rayDistance);
    }
    private void Update()
    {
        // 毎フレーム判定を更新（アニメーターに反映）
        bool grounded = IsGround();
        _animator.SetBool("IsTouchGround", grounded);

    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Ground"))
        {
            _isTouchTheGround = false;
        }
    }
}
