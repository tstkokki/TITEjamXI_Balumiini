using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilWaterScript : MonoBehaviour
{
    public float risingSpeed = 0.5f;
    Vector3 rising;
    

    private void FixedUpdate()
    {
        rising = new Vector3(0, risingSpeed, 0);
        //continuously rising water
        transform.Translate(rising*Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SugarDaddy"))
        {
            //if player is in water, reload scene
            SceneManager.LoadScene(1);
        }
    }
}
