using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractManager : MonoBehaviour
{
    private PlayerMovementScript playerMovementScript;
    
    public LayerMask layerMask;

    public GameObject useText;

    public GameObject playerCamera, catCamera, playerUI;
    private TMP_Text text;

    private float wrongKeyTimer;
    
    [Header("Key Stuff")]
    public bool redKey;
    public bool greenKey;
    public bool yellowKey;

    private void Awake()
    {
        text = useText.GetComponent<TMP_Text>();
        playerMovementScript = GetComponentInParent<PlayerMovementScript>();
        wrongKeyTimer = 0f;
    }
    
    private void Update()
    {
        KeyRaycast();
        HookRaycast();
        
    }

    private void KeyRaycast()
    {
        wrongKeyTimer -= Time.deltaTime;
        
        //Raycast for the Use key (E)
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 3f, layerMask))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Cat")) //If you're looking at the cat, give the player an option to talk to him
            {
                useText.SetActive(true);
                text.text = "E to Talk";

                if (!Input.GetKeyDown(KeyCode.E)) return;
                playerCamera.SetActive(false);
                catCamera.SetActive(true);
                playerUI.SetActive(false);
            }
            
            else if (hit.transform.CompareTag("Door")) //If you're looking at the door, give an option to open it
            {
                useText.SetActive(true);
                text.text = wrongKeyTimer > 0f ? "You don't have the right key" : "E to Open";

                if (!Input.GetKeyDown(KeyCode.E)) return;
                var doorManager = hit.transform.GetComponent<DoorScript>();
                switch (doorManager.doorColor)
                {
                    case KeyColor.Red when redKey:
                    case KeyColor.Green when greenKey:
                    case KeyColor.Yellow when yellowKey:
                        doorManager.Disintegrate();
                        break;
                    default:
                        //Debug.Log("YOU DON'T HAVE THE RIGHT KEY, FUCKER!");
                        wrongKeyTimer = 2f;
                        break;
                }
            }

            else //If you're looking at something that's not interactable, disable the use text
            {
                useText.SetActive(false);
            }


        }
        else //If you're not looking at anything, disable the use text
        {
            useText.SetActive(false);
        }
        
        
        if (!(wrongKeyTimer > 0f)) return; //If you're previously tried to open the door without having the right key, the message lingers
        useText.SetActive(true);
        text.text = "You don't have the right key";
    }

    private void HookRaycast()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMovementScript.grappled)
            playerMovementScript.grappled = false;
            
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 20f, layerMask))
        {
            //Debug.Log(hit.transform.name);
            
            if (hit.transform.CompareTag("Grapple")) //If you're looking at the grapple point, show the little crosshair UI thing
            {
                useText.SetActive(true);
                text.text = "E to Grapple";

                if (Input.GetKeyDown(KeyCode.E) && !playerMovementScript.grappled)
                {
                    playerMovementScript.grappled = true;
                    playerMovementScript.oRb = hit.rigidbody;
                }
                


            }
        }
    }
}
