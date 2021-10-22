using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{

    public float cooldown;
    private float cool;
    private Animator animator;
    private static readonly int Swing = Animator.StringToHash("Swing");
    private bool right = false;
    private static readonly int Right = Animator.StringToHash("Right");
    public CameraShake cameraShake;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        cool -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.E) && cool <= 0f)
        {
            cool = cooldown;
            right = !right;
            animator.SetTrigger(Swing);
            animator.SetBool(Right, right);

            cameraShake.NoiseShake();

        }
    }
}
