using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Master_Script : MonoBehaviour
{
    ObjectPooler platformPooler;
    public int platformCount = 0;
    public int maxCount;
    // Start is called before the first frame update
    void Start()
    {
        platformPooler = GetComponent<ObjectPooler>();
        maxCount = platformPooler.poolAmount;
    }

    public void SpawnNextPlatform(float nextPosition)
    {
        GameObject nextPlat = platformPooler.GetPooledObjecT();
        if(nextPlat == null)
        {
            nextPlat = platformPooler.GetPooledObjecT(true, platformCount);
            platformCount++;
            nextPlat.SetActive(false);
            if (platformCount >= maxCount)
            {
                platformCount = 0;

            }
        }
        nextPlat.SetActive(true);
        int randPos = Random.Range(-10, 10);
        nextPlat.transform.position = new Vector3(randPos, nextPosition+7f, nextPlat.transform.position.z);

        
    }
}
