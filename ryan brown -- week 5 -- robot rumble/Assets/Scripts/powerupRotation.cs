using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupRotation : MonoBehaviour
{
    //----------------------PRIVATE VARIABLES----------------------
    float rotationSpeed = 270f;

    //----------------------UPDATE----------------------
    void Update()
    {
        //----------------------ROTATION----------------------
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
