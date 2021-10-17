using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class DoorScript : MonoBehaviour
{
public PlayerMovementScript playerMovement;

private MeshRenderer meshRenderer;

[Title("Key Color")] [EnumToggleButtons]
public KeyColor doorColor;

private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.material.color = doorColor switch
        {
            KeyColor.Red => Color.red,
            KeyColor.Green => Color.green,
            KeyColor.Yellow => Color.yellow,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (doorColor)
        {
            case KeyColor.Red:
                if (other.transform.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovementScript>().redKey)
                        Destroy(gameObject);
                }
                break;
            case KeyColor.Green:
                if (other.transform.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovementScript>().greenKey)
                        Destroy(gameObject);
                }
                break;
            case KeyColor.Yellow:
                if (other.transform.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovementScript>().yellowKey)
                        Destroy(gameObject);
                }
                break;
        }
        
    }
}
