using Unity.VisualScripting;
using UnityEngine;

public class UpperClamp : MonoBehaviour
{
    [SerializeField, Header("çÇìxå¿äE")]
    private float _ceilMax = 256;
    private PlayerTakeDamage _takeDamage = default;

    private void Start()
    {
        _takeDamage = GetComponent<PlayerTakeDamage>();
    }

    private void FixedUpdate()
    {
            PlayerPositionCorrection();
    }

    private void PlayerPositionCorrection()
    {
        Vector3 curPos = this.transform.position;
        transform.position = new Vector3(curPos.x, Mathf.Min(curPos.y, _ceilMax), curPos.z);
    }
}
