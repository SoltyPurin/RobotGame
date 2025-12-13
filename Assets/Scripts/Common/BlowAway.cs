using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class BlowAway : MonoBehaviour
{
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("ヒットストップさせるフレーム数")]
    private float _stopFlame = 30;
    private float _currentStopTime = 0;
    private bool _canHitStop = false;

    private Vector3 _direction = Vector3.zero;
    private float _blowAwayPower = 0;
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void BlowAwayProtocol(Vector3 direction,float blowAwayPower)
    {
        _direction = direction;
        _blowAwayPower=blowAwayPower;
        _canHitStop = true;
        Time.timeScale = 0.3f;
    }

    private void Update()
    {
        if (!_canHitStop)
        {
            return;
        }
        //Debug.Log("ヒットストップ");
        _currentStopTime += Time.unscaledDeltaTime;
        if( _currentStopTime > 0.3f)
        {
            Time.timeScale = 1;
            _canHitStop = false;
            _onBallRigidBody.AddForce(_direction * _blowAwayPower, ForceMode.Impulse);
        }
    }
}
