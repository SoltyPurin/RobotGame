using UnityEngine;
using UniRx;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("玉のリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("速度")]
    private float _moveSpeed = 50;
    [SerializeField, Header("ダッシュ時に加算する速度")]
    private float _dashPlusValue = 20;
    [SerializeField, Header("重力")]
    private float _downForce = 5;
    [SerializeField,Header("ロックオンカメラ")]
    private GameObject _lockOnCamera = default;
    [SerializeField, Header("ロボットの見た目")]
    private GameObject _robotObj = default;
    [SerializeField, Header("ダッシュのパーティクル")]
    private ParticleSystem _dashParticle = default;

    [SerializeField, Header("地面の検知")]
    private GroundDetect _groundDetect = default;

    private ReactiveProperty<bool> _isRunning = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> IsRunning
    {
        get { return _isRunning; }
    }

    private float _lockYAxis = 0;

    private RaycastHit _hit;
    private float _verticalValue = 0.0f;
    private float _horizontalValue = 0.0f;
    private float _sphereRadius = 0;
    private Vector2 _v2MoveValue = Vector2.zero;
    private Vector3 _useVelocity = Vector3.zero;
    private GameObject _activeCamera = default;
    private LockOn _lockOn = default;
    private PlayerSoundPlayScript _soundPlay = default;
    private MoveGage _moveGage = default;
    private PlayAnimationScript _anim;

    private Transform _rockTransform = default;
    private bool _isBurstPerformancing = false;
    private bool _isLandingAnimation = false;
    public void Initialize(PlayAnimationScript anim,LockOn lockOn,MoveGage moveGage)
    {
        _anim = anim;
        _rockTransform = transform;
        _soundPlay = FindAnyObjectByType<PlayerSoundPlayScript>();
        _dashParticle.Stop();
        _isRunning.Value = false;
        _sphereRadius = _ballRigidBody.gameObject.GetComponent<SphereCollider>().radius + 0.2f;
        _lockOn = lockOn;
        _moveGage = moveGage;
        _activeCamera = _lockOnCamera;
    }
    private void Update()
    {
        Physics.Raycast(_ballRigidBody.position, Vector3.down, out _hit, _sphereRadius);
    }
    public void InputProtocol(Vector2 input)
    {
        _v2MoveValue = input;
        _verticalValue = _v2MoveValue.y;
        _horizontalValue = _v2MoveValue.x;
    }
    /// <summary>
    /// バーストが発動した時と非発動時のフラグを帰る
    /// </summary>
    /// <param name="isActive">バーストが発動したか</param>
    public void InBurstMove(bool isActive)
    {
        if (isActive)
        {
            _isBurstPerformancing = true;
            _rockTransform.position = transform.position;
            _rockTransform.rotation = transform.rotation;
        }
        else
        {
            _isBurstPerformancing = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_isRunning.Value && !_groundDetect.IsGround())
        {
            _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);

        }
        if (_isLandingAnimation)
        {
            return;
        }
            MoveProtocol();
            if (_isRunning.Value)
            {
                YAxisLock();
            }
        if (_isBurstPerformancing)
        {
            transform.position = _rockTransform.position;
            transform.rotation = _rockTransform.rotation;
        }


    }

    public void DashTimeHeal()
    {
        _moveGage.ResetMoveValue();
    }
    public void DashProtocol()
    {
        if (_isRunning.Value)
        {
            DashUnLockProtocol();
        }
        else
        {
            if (_moveGage.MoveTimeProperty.Value >= 0)
            {
                DashOnLineProtocol();
            }
        }
    }

    private void DashOnLineProtocol()
    {
        _soundPlay.PlayDashSound(_isRunning.Value);
        _dashParticle.Play();
        _isRunning.Value = true;
        _lockYAxis = this.transform.position.y;
        _moveSpeed += _dashPlusValue;
        _anim.DashSwitch(true);
    }
    private void DashUnLockProtocol()
    {
        _anim.DashSwitch(false);
        Landing();
        _soundPlay.PlayDashSound(_isRunning.Value);
        _dashParticle.Stop();
        _isRunning.Value = false;
        _moveSpeed -= _dashPlusValue;
    }
    public void Landing()
    {
        _anim.LandingAnim();
        _isLandingAnimation = true;
        StartCoroutine(LandingEndLag());
    }

    private IEnumerator LandingEndLag()
    {
        yield return new WaitForSeconds(0.5f);
        _isLandingAnimation = false;
    }
    private void YAxisLock()
    {
        Vector3 curPos = this.transform.position;
        //if(_lockYAxis<=_groundDetect.GroundYAxis(curPos.x, curPos.z))
        //{
        //    _lockYAxis = _groundDetect.GroundYAxis(curPos.x, curPos.z);
        //    Debug.Log("高さ更新");
        //}
        ////curPos.y = _lockYAxis;
        _onBallRigidBody.MovePosition(curPos);
        _moveGage.Moveing(false);
        if(_moveGage.MoveTimeProperty.Value < 0 )
        {
            DashUnLockProtocol();
        }
    }

    private void MoveProtocol()
    {

        Vector3 curVelocity = _ballRigidBody.linearVelocity;
        if(_lockOn.TargetTransform == null)
        {
            NormalProtocol(_activeCamera, curVelocity);
        }
        else
        {
            LockOnMoveProtocol(_activeCamera, curVelocity);
        }

    }

    private void LockOnMoveProtocol(GameObject activeCamera, Vector3 curVelocity)
    {
        Vector3 targetPos = _lockOn.TargetTransform.position;
        Vector3 dir = targetPos - _onBallRigidBody.position;
        dir.y = 0f;

        //カメラが向いている方向から、上下の傾きを除いた『真前』の方向
        Vector3 cameraForward = Vector3.Scale(activeCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        //今のカメラの向きを基準にして、スティックを倒した方向（ワールド座標）
        Vector3 moveForward = cameraForward * _verticalValue + activeCamera.transform.right * _horizontalValue;
        //カメラを無視した、ワールド座標の固定方向
        Vector3 rotationInput = new Vector3(_horizontalValue, 0, _verticalValue);
        if (_v2MoveValue.magnitude > 0.01f)
        {
            //Debug.Log("移動");
            Quaternion targetRot = Quaternion.LookRotation(rotationInput, Vector3.up);
            _robotObj.transform.localRotation = targetRot;
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            _onBallRigidBody.MoveRotation(rot);
            _useVelocity = moveForward * _moveSpeed /** Time.fixedDeltaTime*/;
            _useVelocity.y = curVelocity.y;
            //_ballRigidBody.AddForce(_useVelocity);
            _ballRigidBody.linearVelocity = _useVelocity;
        }
        else
        {

            _ballRigidBody.linearVelocity *= 0.9f;
        }
    }

    private void NormalProtocol(GameObject activeCamera,Vector3 curVelocity)
    {
        Vector3 cameraForward = Vector3.Scale(activeCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * _verticalValue + activeCamera.transform.right * _horizontalValue;
        _robotObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (_v2MoveValue.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveForward, Vector3.up);
            Quaternion temp = Quaternion.RotateTowards(_onBallRigidBody.rotation, targetRot, 600 * Time.fixedDeltaTime);
            _onBallRigidBody.rotation = temp;
            _useVelocity = moveForward * _moveSpeed * Time.deltaTime;
            _useVelocity.y = curVelocity.y;
            _ballRigidBody.linearVelocity = _useVelocity;
        }
        else
        {
            _ballRigidBody.linearVelocity *= 0.9f;
        }
    }

    public void AuraBurst(int burstValue,bool isPlus)
    {
        if (isPlus)
        {
            _moveSpeed += burstValue;
        }
        else
        {
            _moveSpeed -= burstValue;
        }
    }

}
