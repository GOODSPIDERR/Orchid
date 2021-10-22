using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{

    private Animator animator;
    private static readonly int Swing = Animator.StringToHash("Swing");
    private bool right = false;
    private static readonly int Right = Animator.StringToHash("Right");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            right = !right;
            animator.SetTrigger(Swing);
            animator.SetBool(Right, right);
            
        }
    }
}
