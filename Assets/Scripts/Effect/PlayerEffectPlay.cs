using UnityEngine;

public class PlayerEffectPlay : MonoBehaviour
{
    [SerializeField, Header("右スラスターのエフェクト")]
    private ParticleSystem _rightThruster = default;
    [SerializeField, Header("左スラスターのエフェクト")]
    private ParticleSystem _leftThruster = default;
    private void Awake()
    {
        _rightThruster.Stop();
        _leftThruster.Stop();
    }

    public void PlayThrusterEffect()
    {
        _rightThruster.Play();
        _leftThruster.Play();
    }

    public void StopThrusterEffect()
    {
        _rightThruster.Stop();
        _leftThruster.Stop();
    }
}
