using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimations : MonoBehaviour
{
    Animator animator;
    public BoxCollider torso, head, armL, armR, legL, legR;
    BoxCollider hitCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void BodyPartHit(BoxCollider boxCollider)
    {
        if (boxCollider == torso)
        {
            animator.SetTrigger("Torso Hit");
            Debug.Log("Torso Hit!");
        }
        else if (boxCollider == head)
        {
            animator.SetTrigger("Head Hit");
            Debug.Log("Head Hit!");
        }
        else if (boxCollider == armL)
        {
            animator.SetTrigger("Arm L Hit");
            Debug.Log("Left Arm Hit!");
        }
        else if (boxCollider == armR)
        {
            animator.SetTrigger("Arm R Hit");
            Debug.Log("Right Arm Hit!");
        }
        else if (boxCollider == legL)
        {
            animator.SetTrigger("Leg L Hit");
            Debug.Log("Torso Hit!");
        }
        else if (boxCollider == legR)
        {
            animator.SetTrigger("Leg R Hit");
            Debug.Log("Right Leg Hit!");
        }
    }
}
