using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Collections;

public class BulletPool : MonoBehaviour
{
    [SerializeField, Header("ê∂ê¨Ç∑ÇÈèeíe")]
    private GameObject _bullet = default;
    [SerializeField, Header("ê∂ê¨Ç∑ÇÈå¬êî")]
    private int _bulletGenerateCount = 100;

    private List<GameObject> _bulletList = new List<GameObject>();
    private List<BulletMove> _bulletMove = new List<BulletMove>();

    private int _currentActiveBulletIndex = 0;

    private void Awake()
    {
        for(int i =0; i < _bulletGenerateCount; i++)
        {
            GameObject bullet = Instantiate(_bullet, this.transform);
            _bulletList.Add(bullet);
            BulletMove move = bullet.GetComponent<BulletMove>();
            _bulletMove.Add(move);
            bullet.SetActive(false);
        }
    }

    public void ActiveBullet(Vector3 targetDir,float bulletAliveTime,
        Vector3 startPoint,int bulletDamage,float blowAwayPower)
    {
        if(_currentActiveBulletIndex >= _bulletList.Count)
        {
            _currentActiveBulletIndex = 0;
        }

        _bulletList[_currentActiveBulletIndex].SetActive(true);
        _bulletList[_currentActiveBulletIndex].transform.position = startPoint;
        _bulletMove[_currentActiveBulletIndex].StartMove(targetDir,bulletDamage,blowAwayPower,_currentActiveBulletIndex);
        StartCoroutine(LifeTimeDeActiveBullet(bulletAliveTime,_currentActiveBulletIndex));
        _currentActiveBulletIndex++;
    }

    private IEnumerator LifeTimeDeActiveBullet(float aliveTime,int activeBulletIndex)
    {
        yield return new WaitForSeconds(aliveTime);
        if (_bulletList[activeBulletIndex].activeInHierarchy)
        {
            _bulletList[activeBulletIndex].SetActive(false);
        }
    }

    public void DeActiveBullet(GameObject bullet)
    {
        if (bullet.activeInHierarchy)
        {
            Debug.Log("èeíeçÌèú");
            BulletMove bMove = bullet.GetComponent<BulletMove>();
            int index = bMove.BulletIndex;
            _bulletList[index].SetActive(false);
        }
    }
}
