using System.Diagnostics.Eventing.Reader;
using UnityEditor;
using UnityEngine;

public class PlayerGrappleState : PlayerBaseState
{
    private Rigidbody rb;
    private Rigidbody oRb;
    private SpringJoint joint;
    private Transform transform;
    public override void EnterState(PlayerMovementScript player)
    {
        //Getters
        rb = player.GetComponent<Rigidbody>();
        joint = player.GetComponent<SpringJoint>();
        transform = player.transform;
        
        //RB Setup
        oRb = player.oRb;
        rb.isKinematic = false;
        player.move = new Vector3(0, 0, 0);

        var difference = transform.position - oRb.position;
        var mag = difference.magnitude;
        
        //Spring Joint Setup
        joint.connectedBody = player.oRb;
        joint.spring = 20f;
        joint.damper = 10;
        joint.maxDistance = Mathf.Abs(mag)/2; //Change this to depend on the distance between the player and the collision point
        joint.minDistance = 1f;
        joint.anchor = Vector3.up;
        joint.connectedAnchor = new Vector3(0, -mag/2, 0);
        

    }
    
    public override void UpdateState(PlayerMovementScript player)
    {
        player.move = new Vector3(0, 0, 0);

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        rb.AddForce(player.transform.forward * verticalInput * player.swingForce * Time.deltaTime);
        rb.AddForce(player.transform.right * horizontalInput * player.swingForce * Time.deltaTime);

        if (player.isGrounded)
        {
            player.SwitchState(player.MoveState);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            player.ReturnHook();
            player.SwitchState(player.FlyState);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 12f + transform.forward * 6f, ForceMode.VelocityChange);
            
            player.ReturnHook();
            player.SwitchState(player.FlyState);
        }
    }
}
