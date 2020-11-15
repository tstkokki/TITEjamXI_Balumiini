using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSugarScript : MonoBehaviour
{
    Animator sugarani;
    // Start is called before the first frame update
    void Start()
    {
        sugarani = GetComponent<Animator>();
        sugarani.Play("metarig|Mainmenu", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
