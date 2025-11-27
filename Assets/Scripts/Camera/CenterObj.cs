using UnityEngine;

public class CenterObj : MonoBehaviour
{
    [SerializeField, Header("èâä˙ç¿ïW")]
    private Vector3 _firstPos = default;

    private GameObject _player = default;
    private GameObject _target = default;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        Vector3 plPos = _player.transform.position;
        plPos.y += _firstPos.y;
        plPos.z += _firstPos.z;
        transform.position = plPos;

    }
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if(_target != null)
        {
            Debug.Log("íÜä‘ínì_Ç…Ç¢ÇÈ");
            transform.position = CalcMidPoint(_target, _player);
        }
        else
        {
            Vector3 plPos = _player.transform.position;
            plPos.y += _firstPos.y;
            plPos.z += _firstPos.z;
            transform.position = plPos;
        }
    }

    private Vector3 CalcMidPoint(GameObject target,GameObject player)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 playerPos = player.transform.position;
        float retX = (targetPos.x + playerPos.x) / 2;
        float retY = (targetPos.y + playerPos.y) / 2;
        float retZ = (targetPos.z + playerPos.z) / 2;

        return new Vector3(retX, retY, retZ);
    }
}
