using UnityEngine;

public class PlayerFlyState : PlayerBaseState
{
    private Rigidbody rb;
    private bool isGrounded;
    private float velocityY;
    public override void EnterState(PlayerMovementScript player)
    {
        rb = player.rb;
        velocityY = rb.velocity.y;
    }
    
    public override void UpdateState(PlayerMovementScript player)
    {
        player.move = new Vector3(0, 0, 0);
        
        isGrounded = player.isGrounded;
        velocityY = rb.velocity.y;

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        rb.AddForce(player.transform.forward * verticalInput * player.swingForce * Time.deltaTime);
        rb.AddForce(player.transform.right * horizontalInput * player.swingForce * Time.deltaTime);

        if (isGrounded)
        {
            player.SwitchState(player.MoveState);
            player.move = new Vector3(player.move.x, 0, player.move.z);
        }
        
        if(player.grappled && velocityY <= -4f)
        {
            player.SwitchState(player.GrappleState);
        }
    }
}
