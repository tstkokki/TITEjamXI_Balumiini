using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWaterScript : MonoBehaviour
{
    public float risingSpeed = 0.5f;
    Vector3 rising;
    public bool touchWater = false;

    private void FixedUpdate()
    {
        if (!touchWater)
        {
            rising = new Vector3(0, risingSpeed, 0);
            //continuously rising water
            transform.Translate(rising*Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SugarDaddy") && !touchWater)
        {
            touchWater = true;

            //if player is in water, reload scene
            other.gameObject.GetComponent<Player_Controller_Script>().KillPlayer();
            //SceneManager.LoadScene(1);
        }
    }

    public void IncreaseTide(float _speedUp)
    {
        risingSpeed += _speedUp/10;
        Debug.Log(risingSpeed);
    }
}
