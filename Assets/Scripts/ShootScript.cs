using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootScript : MonoBehaviour
{
    //I'm so sorry that declarations are so disorganized
    //Wait these ones are actually fairly organized. They're not as organized inside other scripts though
    public Transform mainCamera;
    public GameObject weapon1, weapon2;
    public LayerMask canHit;
    public AudioSource shotSound;
    public float damage = 1f;
    [Header("Muzzle Flash and Impact")]
    [Range(0.0f, 50.0f)]
    public float knockback = 0f;
    public GameObject impactVFX;
    public Transform muzzleFlashSpot;
    public GameObject muzzleFlashObject;
    [Header("Screenshake Options")]
    public float shakeDuration = 0f;
    public Vector3 shakeStrength = new Vector3(0, 0, 0);
    public int shakeFrequency = 0;
    public float shakeRandomness = 0f;
    [Header("Bools for the animator")]
    public bool canShoot = false;
    public bool canSwitch = false;
    Animator animator;
    float maxHitDistance = 2000f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canShoot)
        {
            if (Input.GetButton("Fire1")) //Responsible for everything that happens when you fire
            {
                //Camera shake
                mainCamera.localPosition = new Vector3(0, 0.891f, 0);
                Sequence shakeSequence = DOTween.Sequence();
                shakeSequence.Append(mainCamera.DOShakePosition(shakeDuration, shakeStrength, shakeFrequency, shakeRandomness, false, true));
                shakeSequence.Append(mainCamera.DOLocalMove(new Vector3(0, 0.891f, 0), 0.4f));
                shakeSequence.Play();

                //Creates the muzzle flash
                Instantiate(muzzleFlashObject, muzzleFlashSpot);

                //Plays the shooting sound and randomizes the pitch slightly
                shotSound.pitch = Random.Range(0.95f, 1.05f);
                shotSound.Play();

                //Sets off the animation trigger
                animator.SetTrigger("Shoot");

                //Finds where the bullet lands
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.position, mainCamera.TransformDirection(Vector3.forward), out hit, maxHitDistance, canHit))
                {
                    //If the thing you hit happens to have a rigidbody, applies a force in the direction you're facing
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                        Vector3 direction = (transform.position - hit.transform.position).normalized;
                        rb.AddForce(-direction * knockback, ForceMode.Impulse);
                    }
                    //Creates the burst VFX
                    GameObject vfx = Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal, Vector3.back));
                    vfx.transform.parent = hit.transform;

                    //If it hits an enemy, do damage with its script
                    if (hit.transform.tag == "Enemy")
                    {
                        //Finds an EnemyScript component in the enemy and do damage to it
                        EnemyScript enemyScript = hit.transform.gameObject.GetComponentInParent<EnemyScript>();
                        enemyScript.TakeDamage(damage);

                        //Finds the collider that it hit, and plays the hit animation depending on the collider it hit
                        HitAnimations hitAnimations = hit.transform.gameObject.GetComponentInParent<HitAnimations>();
                        BoxCollider colliderHit = hit.transform.gameObject.GetComponent<BoxCollider>();
                        hitAnimations.BodyPartHit(colliderHit);
                    }
                }
            }


        }

        if (canSwitch) //Weapon switching (btw if you're better with the new input system, we should switch to the new input system. I can't be asked to learn it rn, but from what I've seen it seems a lot better than the old one ok rant over)
        {
            if (Input.GetButtonDown("Weapon1"))
            {
                weapon1.SetActive(true);
                weapon2.SetActive(false);

            }

            if (Input.GetButtonDown("Weapon2"))
            {
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            }
        }


    }
}
