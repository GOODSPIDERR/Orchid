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
        float movementX = -Input.GetAxisRaw("Mouse X") * amount;
        float movementY = -Input.GetAxisRaw("Mouse Y") * amount;

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
