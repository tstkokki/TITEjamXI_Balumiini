using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler PoolInstance;
    public List<GameObject> poolList;
    public GameObject pooledObject;
    public int poolAmount;

    private void Awake()
    {
        PoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        poolList = new List<GameObject>();
        for(int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject GetPooledObjecT(bool isDisabling = false, int stepCount = 0)
    {
        if (isDisabling)
        {
            return poolList[stepCount];
        } else
        {
            for (int i = 0;i < poolList.Count; i++)
            {
                if (!isDisabling)
                {
                
                    if (!poolList[i].activeInHierarchy)
                    {
                        return poolList[i];
                    }
                }
            }
            return null;
        }
    }
}
