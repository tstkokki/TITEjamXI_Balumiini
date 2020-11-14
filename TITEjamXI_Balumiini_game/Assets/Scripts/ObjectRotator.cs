using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    Vector3 rotationParam = Vector3.up;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationParam); 
    }
}
