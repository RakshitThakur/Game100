using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    List<GameObject> pooledObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;
    void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject tmp = (GameObject)Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
