using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float amount;
    public float smoothAmount;
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        //I think I stole this from a YouTube tutorial. Works nicely though. Just make sure you don't attach this script to the same object as the SineSway.cs, or else fucky wucky will happen
        float movementX = -Input.GetAxisRaw("Mouse X") * amount;
        float movementY = -Input.GetAxisRaw("Mouse Y") * amount;

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
