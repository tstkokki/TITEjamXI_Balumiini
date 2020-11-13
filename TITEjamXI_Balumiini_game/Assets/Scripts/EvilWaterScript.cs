using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilWaterScript : MonoBehaviour
{
    Vector3 rising = new Vector3(0, 0.2f, 0);
    

    private void FixedUpdate()
    {
        transform.Translate(rising*Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SugarDaddy"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
