using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerMovementScript : MonoBehaviour
{
    //This script and the script for all the other player states needs some serious re-organizing
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public Vector3 move;

    public Transform lineOrigin;
    public SpringJoint joint;

    [HideInInspector] public CapsuleCollider capsuleCollider;

    //[HideInInspector] 
    public bool grappled = false;
    
    [Header("Movement Stuff")]
    public float moveSpeed = 8f;
    public float acceleration = 8f;
    
    [Header("Jumping & Gravity Stuff")]
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float velocityX, velocityZ;

    [HideInInspector]
    public Vector3 velocity;
    public bool isGrounded;
    
    public float swingForce = 500f;

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Rigidbody oRb;

    public Transform hook;
    public Vector3 hookInitialPosition;
    public Quaternion hookInitialRotation;
    public Transform hand;

    PlayerBaseState currentState;
    public PlayerMoveState MoveState = new PlayerMoveState();
    public PlayerGrappleState GrappleState = new PlayerGrappleState();
    public PlayerFlyState FlyState = new PlayerFlyState();
    public PlayerCutsceneState CutsceneState = new PlayerCutsceneState();

    [HideInInspector]
    public LineRenderer lineRenderer;
    
    private void Awake()
    {
        //Getter
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<SpringJoint>();
    }

    private void Start()
    {
        currentState = MoveState;
        
        currentState.EnterState(this);

        hookInitialPosition = hook.localPosition;
        hookInitialRotation = hook.localRotation;
    }

    private void Update()
    {
        currentState.UpdateState(this);

        //Debug.Log(move);
        
        //Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        
    }

    private void LateUpdate()
    {
        var points = new Vector3[2];
        points[0] = lineOrigin.position;
        points[1] = hook.position;
        lineRenderer.SetPositions(points);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void ReturnHook()
    {
        joint.connectedBody = null;
        joint.maxDistance = 9999f;
        joint.spring = 0f;
        joint.damper = 0f;
        
        grappled = false;
        
        hook.parent = hand;
        hook.DOLocalMove(hookInitialPosition, 0.2f);
        hook.localRotation = hookInitialRotation;
    }
}
