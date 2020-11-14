using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Phasing_Script : MonoBehaviour
{
    private new Collider collider;
    private float liftTimer = 0f;
    private bool setTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = transform.parent.GetComponent<Collider>();
        //if (collider != null) Debug.Log("Collider found");
    }

    // Update is called once per frame
    void Update()
    {
        if (setTrigger) TriggerDelay();
    }

    void TriggerDelay()
    {
        liftTimer += Time.deltaTime;
        if (liftTimer >= 0.5f && setTrigger)
        {
            setTrigger = false;
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if player's foot touches top part of platform, set collider to solid
            collider.isTrigger = false;
            setTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            setTrigger = true;
            liftTimer = 0f;
        }
    }
}
