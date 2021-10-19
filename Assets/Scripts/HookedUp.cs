using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookedUp : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float swingForce = 10f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        rb.AddForce(transform.forward * verticalInput * swingForce * Time.deltaTime);
        rb.AddForce(transform.right * horizontalInput * swingForce * Time.deltaTime);
    }
}
