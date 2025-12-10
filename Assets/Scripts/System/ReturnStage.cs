using UnityEngine;

public class ReturnStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        Vector3 curPos = obj.transform.position;
        curPos.y += 10f;
        obj.transform.position = curPos;
    }
}
