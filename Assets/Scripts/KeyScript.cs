using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum KeyColor
{
    Red,
    Green,
    Yellow
}
public class KeyScript : MonoBehaviour
{
public PlayerMovementScript playerMovement;

public float rotationRate = 1f;
public float yOffset = 1f;

private MeshRenderer meshRenderer;
[Title("Key Color")]
[EnumToggleButtons]
public KeyColor selectedColor;

private float stopwatch = 0f;

private Vector3 initialPosition;

private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.material.color = selectedColor switch
        {
            KeyColor.Red => Color.red,
            KeyColor.Green => Color.green,
            KeyColor.Yellow => Color.yellow,
            _ => throw new ArgumentOutOfRangeException()
        };

        initialPosition = transform.position;
    }
    
    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationRate, 0));
        stopwatch += Time.deltaTime;
        var interpolation = Mathf.Sin(stopwatch);

        transform.position = Vector3.Lerp(initialPosition, new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z), Mathf.Abs(interpolation));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        switch (selectedColor)
        {
            case KeyColor.Red:
                playerMovement.redKey = true;
                break;
            case KeyColor.Green:
                playerMovement.greenKey = true;
                break;
            case KeyColor.Yellow:
                playerMovement.yellowKey = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Destroy(gameObject);
    }
}
