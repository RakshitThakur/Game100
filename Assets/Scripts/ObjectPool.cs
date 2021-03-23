using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    List<GameObject> pooledBullets;
    List<GameObject> pooledEnemy;
    [SerializeField] GameObject bulletToPool;
    [SerializeField] GameObject enemyToPool;
    [SerializeField] int amountToPool;
    void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledBullets = new List<GameObject>();
        pooledEnemy = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject tmp = (GameObject)Instantiate(bulletToPool);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
            tmp = (GameObject)Instantiate(enemyToPool);
            tmp.SetActive(false);
            pooledEnemy.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetPooledBullet()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledEnemy[i].activeInHierarchy)
            {
                return pooledEnemy[i];
            }
        }
        return null;
    }
   
}
