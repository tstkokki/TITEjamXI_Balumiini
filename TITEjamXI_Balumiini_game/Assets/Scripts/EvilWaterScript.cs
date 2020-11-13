using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWaterScript : MonoBehaviour
{
    Vector3 rising = new Vector3(0, 0.2f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(rising*Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
