using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHover : MonoBehaviour
{
    public GameObject ControlsText;
    void Start()
    {
        ControlsText.SetActive(false);
    }

    public void OnMouseEnter()
    {
        ControlsText.SetActive(true);
    }

    public void OnMouseExit()
    {
        ControlsText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
