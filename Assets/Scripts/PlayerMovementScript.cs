using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    CharacterController controller;
    [HideInInspector]
    public Vector3 move;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    private void Start()
    {
        //Getter
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        //Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If grounded, stop falling. Otherwise add y velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Moving
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
