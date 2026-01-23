using UnityEngine;

public class BlowAwayGroundCheck : MonoBehaviour
{
    [SerializeField, Header("被弾スクリプト")]
    private PlayerTakeDamage _takeDamage = default;

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
