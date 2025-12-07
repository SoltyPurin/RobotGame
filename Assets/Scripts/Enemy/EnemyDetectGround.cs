using UnityEngine;

public class EnemyDetectGround : MonoBehaviour
{
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
