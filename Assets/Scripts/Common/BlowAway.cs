using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class BlowAway : MonoBehaviour
{
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("ヒットストップさせるフレーム数")]
    private float _stopTime = 30;
    [SerializeField, Header("隠し味に少しだけ浮かせる力")]
    private float _vUpPower = 0.2f;
    private float _currentStopTime = 0;
    private bool _canHitStop = false;

    private Vector3 _direction = Vector3.zero;
    private float _blowAwayPower = 0;

    private AuraBurstPerformance _performance = default;
    private void Start()
    {
        Time.timeScale = 1;
        _performance = FindAnyObjectByType<AuraBurstPerformance>();
    }
    public void BlowAwayProtocol(Vector3 direction,float blowAwayPower)
    {
        _direction = direction;
        _blowAwayPower=blowAwayPower;
        _canHitStop = true;
        //Time.timeScale = 0.3f;
    }

    private void FixedUpdate()
    {
        if (!_canHitStop)
        {
            return;
        }
        _currentStopTime += Time.unscaledDeltaTime;
        if( _currentStopTime > _stopTime)
        {
            //if (!_performance.IsBurstPerformancing)
            //{
            //    Time.timeScale = 1;
            //}
            _canHitStop = false;
            _direction.y = 0;
            _direction = _direction.normalized;
            _direction.y = _vUpPower;
            _onBallRigidBody.AddForce(_direction * _blowAwayPower, ForceMode.Impulse);
        }
    }
}
