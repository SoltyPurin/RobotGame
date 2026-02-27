using UnityEngine;

public class PlayerEffectPlay : MonoBehaviour
{
    [SerializeField, Header("背部スラスター")]
    private ParticleSystem _rightThruster = default;
    private void Awake()
    {
        _rightThruster.Stop();
    }

    public void PlayThrusterEffect()
    {
        _rightThruster.Play();
    }

    public void StopThrusterEffect()
    {
        _rightThruster.Stop();
    }
}
