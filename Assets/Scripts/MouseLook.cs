using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;

    float mouseY = 0f;

    public Transform playerBody;
    void Start()
    {
        //Makes sure that the cursor is locked when gamening
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        //Finding the mouse input
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float mouseX = playerBody.transform.localEulerAngles.y + Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        //Sets the new x rotation and makes sure it's clamped between -90 and 90
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        //Functions that actually do the rotation
        playerBody.transform.localEulerAngles = new Vector3(0, mouseX, 0);
        transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);
    }
}
