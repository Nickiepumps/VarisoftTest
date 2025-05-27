using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject magicBulletPrefab;
    public GameObject[] magicBulletArr { get; private set; }
    [SerializeField] private int poolSize;
    private void Start()
    {
        magicBulletArr = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(magicBulletPrefab);
            magicBulletArr[i] = bullet;
            magicBulletArr[i].SetActive(false);
        }
    }
    public GameObject CheckAvailableBullet()
    {
        for(int i = 0; i < poolSize; i++)
        {
            if (magicBulletArr[i].activeSelf == false)
            {
                return magicBulletArr[i];
            }
        }
        return null;
    }
}
