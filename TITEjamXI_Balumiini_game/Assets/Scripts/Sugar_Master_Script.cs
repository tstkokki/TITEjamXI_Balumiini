using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar_Master_Script : MonoBehaviour
{
    ObjectPooler sugarPooler;
    public int sugarCount = 0;
    public int maxCount;
    public int sugarIteration = 0;
    // Start is called before the first frame update
    void Start()
    {
        sugarPooler = GetComponent<ObjectPooler>();
        maxCount = sugarPooler.poolAmount;
    }

    public void SpawnNextPlatform(float nextPosition)
    {
        GameObject nextPlat = sugarPooler.GetPooledObjecT();
        if (nextPlat == null)
        {
            nextPlat = sugarPooler.GetPooledObjecT(true, sugarCount);
            sugarCount++;
            nextPlat.SetActive(false);
            if (sugarCount >= maxCount)
            {
                sugarCount = 0;

            }
        }
        nextPlat.SetActive(true);
        nextPlat.SetActive(true);
        int randPos = Random.Range(-7, 7);
        nextPlat.transform.position = new Vector3(randPos, nextPosition + 9f, nextPlat.transform.position.z);
    }
}
