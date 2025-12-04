using UnityEngine;
using System.Collections.Generic;

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

    public void ActiveBullet(Vector3 targetDir)
    {
        if(_currentActiveBulletIndex >= _bulletList.Count)
        {
            _currentActiveBulletIndex = 0;
        }

        _bulletList[_currentActiveBulletIndex].SetActive(true);
        _bulletMove[_currentActiveBulletIndex].StartMove(targetDir);
        _currentActiveBulletIndex++;
    }
}
