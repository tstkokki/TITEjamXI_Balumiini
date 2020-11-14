using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Master_Script : MonoBehaviour
{
    ObjectPooler wallPooler;
    public int wallCount = 0;
    public int maxCount;
    public int wallIteration = 0;
    // Start is called before the first frame update
    void Start()
    {
        wallPooler = GetComponent<ObjectPooler>();
        maxCount = wallPooler.poolAmount;
        SpawnNextPlatform();
    }

    public void SpawnNextPlatform()
    {
        Debug.Log("Called walls");
        GameObject nextPlat = wallPooler.GetPooledObjecT();
        if (nextPlat == null)
        {
            nextPlat = wallPooler.GetPooledObjecT(true, wallCount);
            wallCount++;
            nextPlat.SetActive(false);
            if (wallCount >= maxCount)
            {
                wallCount = 0;

            }
        }
        if(wallIteration < maxCount) wallIteration++;
        nextPlat.SetActive(true);
        nextPlat.transform.position = new Vector3(nextPlat.transform.position.x, nextPlat.transform.position.y + (20f * wallIteration), nextPlat.transform.position.z);

    }
}
