using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    CharacterController controller;
    [HideInInspector]
    public Vector3 move;
    public float moveSpeed = 8f;
    public float acceleration = 8f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    float velocityX, velocityZ;

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
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x == 0f)
        {
            velocityX *= acceleration;
        }


        if (z == 0f)
        {
            velocityZ *= acceleration;
        }

        velocityX += x * acceleration;
        velocityZ += z * acceleration;

        velocityX = Mathf.Clamp(velocityX, -moveSpeed, moveSpeed);
        velocityZ = Mathf.Clamp(velocityZ, -moveSpeed, moveSpeed);

        move = transform.right * velocityX + transform.forward * velocityZ;

        controller.Move(move * Time.deltaTime);

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
