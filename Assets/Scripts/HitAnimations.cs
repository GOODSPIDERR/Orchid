using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to an enemy with hit animations

public class HitAnimations : MonoBehaviour
{
    private Animator animator;
    public BoxCollider torso, head, armL, armR, legL, legR;
    private BoxCollider hitCollider;
    private static readonly int Property = Animator.StringToHash("Torso Hit");
    private static readonly int Property1 = Animator.StringToHash("Head Hit");
    private static readonly int Property2 = Animator.StringToHash("Arm L Hit");
    private static readonly int Property3 = Animator.StringToHash("Arm R Hit");
    private static readonly int Property4 = Animator.StringToHash("Leg L Hit");
    private static readonly int Property5 = Animator.StringToHash("Leg R Hit");

    public HitAnimations(BoxCollider hitCollider)
    {
        this.hitCollider = hitCollider;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BodyPartHit(BoxCollider boxCollider)
    {
        //This is fucking stupid. I'll make Gavin take a look at this later because there's probably a more efficient way of doing it
        if (boxCollider == torso)
        {
            animator.SetTrigger(Property);
            Debug.Log("Torso Hit!");
        }
        else if (boxCollider == head)
        {
            animator.SetTrigger(Property1);
            Debug.Log("Head Hit!");
        }
        else if (boxCollider == armL)
        {
            animator.SetTrigger(Property2);
            Debug.Log("Left Arm Hit!");
        }
        else if (boxCollider == armR)
        {
            animator.SetTrigger(Property3);
            Debug.Log("Right Arm Hit!");
        }
        else if (boxCollider == legL)
        {
            animator.SetTrigger(Property4);
            Debug.Log("Torso Hit!");
        }
        else if (boxCollider == legR)
        {
            animator.SetTrigger(Property5);
            Debug.Log("Right Leg Hit!");
        }
    }
}
