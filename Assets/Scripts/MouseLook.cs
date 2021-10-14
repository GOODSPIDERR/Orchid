using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;

    float mouseY = 0f;

    public Transform playerBody;

    public LayerMask layerMask;

    public GameObject useText;

    public GameObject playerCamera, catCamera, playerUI;
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

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.CompareTag("Cat"))
            {
                useText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerCamera.SetActive(false);
                    catCamera.SetActive(true);
                    playerUI.SetActive(false);
                }
            }

            else
            {
                useText.SetActive(false);
            }
        }
    }
}
