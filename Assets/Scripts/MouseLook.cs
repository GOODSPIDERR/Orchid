using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;

    private float mouseY = 0f;

    public Transform playerBody;

    public LayerMask layerMask;

    public GameObject useText;

    public GameObject playerCamera, catCamera, playerUI;

    private void Start()
    {
        //Makes sure that the cursor is locked when gamening
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Finding the mouse input
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        var mouseX = playerBody.transform.localEulerAngles.y + Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        //Sets the new x rotation and makes sure it's clamped between -90 and 90
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        //Functions that actually do the rotation
        playerBody.transform.localEulerAngles = new Vector3(0, mouseX, 0);
        transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);

        //Raycast for the Use key (E)
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2f, layerMask))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Cat")) //If you're looking at the cat, give the player an option to talk to him
            {
                useText.SetActive(true);

                if (!Input.GetKeyDown(KeyCode.E)) return;
                playerCamera.SetActive(false);
                catCamera.SetActive(true);
                playerUI.SetActive(false);
            }

            else //If you're looking at something that's not the cat, disable the use text
            {
                useText.SetActive(false);
            }


        }
        else //If you're not looking at anything, disable the use text
        {
            useText.SetActive(false);
        }
    }


}
