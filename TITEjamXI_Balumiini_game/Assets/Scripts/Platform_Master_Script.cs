using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Master_Script : MonoBehaviour
{
    ObjectPooler platformPooler;
    public int platformCount = 0;
    public int maxCount;
    public int platScale = 0;
    // Start is called before the first frame update
    void Start()
    {
        platformPooler = GetComponent<ObjectPooler>();
        maxCount = platformPooler.poolAmount;
    }

    public void SetPlatScale(int _scale)
    {
        platScale = _scale;
    }

    public void SpawnNextPlatform(float nextPosition)
    {
        GameObject nextPlat = platformPooler.GetPooledObjecT();
        if(nextPlat == null)
        {
            nextPlat = platformPooler.GetPooledObjecT(true, platformCount);
            platformCount++;
            if(!Mathf.Approximately(nextPlat.transform.localScale.x, (10- (platScale*0.5f))))
            {
                Debug.Log(platScale + " plat doesn't match" + nextPlat.transform.localScale.x);
                nextPlat.transform.localScale = new Vector3(nextPlat.transform.localScale.x-(platScale*0.5f), 0.5f, 1);
            }
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
