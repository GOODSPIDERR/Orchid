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

        Vector2 movementVector = new Vector2(x, z).normalized;

        //Makeshift deceleration solution. There must be a more efficient solution
        if (x == 0f) //Decelerate if not moving X
        {
            velocityX *= acceleration;
        }
        if (z == 0f) //Decelerate if not moving Z
        {
            velocityZ *= acceleration;
        }

        //I decided to slightly rework how movement is calculated. Looks janky, but feels better
        velocityX += movementVector.x * acceleration;
        velocityZ += movementVector.y * acceleration;

        velocityX = Mathf.Clamp(velocityX, -moveSpeed, moveSpeed);
        velocityZ = Mathf.Clamp(velocityZ, -moveSpeed, moveSpeed);

        move = transform.right * velocityX + transform.forward * velocityZ;

        controller.Move(move * Time.deltaTime); //I'm a fucking god

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
