using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ReturnToMonkey : MonoBehaviour
{
    [Required("Must set both cameras")] public GameObject cameraCat, cameraPlayer, ui;

    private UIFunctions uiFunctions;

    
    private void Awake()
    {
        uiFunctions = ui.GetComponent<UIFunctions>();
    }

    [ButtonGroup]
    public void ReturnToMonke()
    {
        cameraCat.SetActive(false);
        cameraPlayer.SetActive(true);
        ui.SetActive(true);
        uiFunctions.UnletterBox();
    }
}
