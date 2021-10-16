using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerMovementScript : MonoBehaviour
{
    private CharacterController controller;
    [HideInInspector]
    public Vector3 move;
    public float moveSpeed = 8f;
    public float acceleration = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float velocityX, velocityZ;

    private Vector3 velocity;
    private bool isGrounded;
    private void Start()
    {
        //Getter
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If grounded, stop falling. Otherwise add y velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Moving
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        var movementVector = new Vector2(x, z).normalized;

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

        var transform1 = transform;
        move = transform1.right * velocityX + transform1.forward * velocityZ;

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
