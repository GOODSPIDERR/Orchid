using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class PlayerGrappleState : PlayerBaseState
{
    private Rigidbody rb;
    private Rigidbody oRb;
    private SpringJoint joint;
    private Transform transform;
    private LineRenderer lineRenderer;
    public override void EnterState(PlayerMovementScript player)
    {
        //Getters
        rb = player.GetComponent<Rigidbody>();
        joint = player.GetComponent<SpringJoint>();
        transform = player.transform;
        
        //RB Setup
        oRb = player.oRb;
        rb.isKinematic = false;
        rb.velocity = new Vector3(player.move.x, player.velocity.y, player.move.z);
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

        lineRenderer = player.lineRenderer;





    }
    
    public override void UpdateState(PlayerMovementScript player)
    {
        player.move = new Vector3(0, 0, 0);
        
        var points = new Vector3[2];
        points[0] = transform.position;
        points[1] = oRb.position;
        player.lineRenderer.SetPositions(points);
        
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
            joint.connectedBody = null;
            joint.maxDistance = 9999f;
            joint.spring = 0f;
            rb.velocity *= 2;
            player.grappled = false;
            player.SwitchState(player.FlyState);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            joint.connectedBody = null;
            joint.maxDistance = 9999f;
            joint.spring = 0f;
            rb.velocity = new Vector3(rb.velocity.x*2, 0, rb.velocity.z*2);
            rb.AddForce(Vector3.up * 16f + transform.forward * 8f, ForceMode.VelocityChange);
            player.grappled = false;
            player.SwitchState(player.FlyState);
        }
    }
}
