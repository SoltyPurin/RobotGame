using UnityEngine;

public class SearchNearEnemy : MonoBehaviour
{
    public GameObject SearchAndReturnNearEnemy()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 curPos = this.transform.position;
        float nearDistance = Vector3.Distance(curPos, enemys[0].transform.position);
        int nearObjIndex = 0;
        for(int i = 1; i < enemys.Length; i++)
        {
            float curDistance = Vector3.Distance(curPos, enemys[i].transform.position);
            if(curDistance < nearDistance)
            {
                nearDistance = curDistance;
                nearObjIndex = i;
            }
        }

        return enemys[nearObjIndex];
    }
}
