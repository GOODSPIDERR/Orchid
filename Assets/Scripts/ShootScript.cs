using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class ShootScript : MonoBehaviour
{
    //I'm so sorry that declarations are so disorganized
    //Wait these ones are actually fairly organized. They're not as organized inside other scripts though
    public Transform mainCamera;
    public LayerMask canHit;
    public AudioSource shotSound;
    public float damage = 1f;
    public float cooldown;
    private float cooldownTimer;
    
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
    

    private bool canShoot = false;
    private Animator animator;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private const float MAXHitDistance = 2000f;




    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        cooldownTimer = cooldown;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        
        canShoot = cooldownTimer < 0f;
        
        if (canShoot)
        {
            if (Input.GetButton("Fire1")) //Responsible for everything that happens when you fire
            {
                cooldownTimer = cooldown;
                
                //Camera shake
                mainCamera.localPosition = new Vector3(0, 0.891f, 0);
                var shakeSequence = DOTween.Sequence();
                shakeSequence.Append(mainCamera.DOShakePosition(shakeDuration, shakeStrength, shakeFrequency, shakeRandomness, false, true));
                shakeSequence.Append(mainCamera.DOLocalMove(new Vector3(0, 0.891f, 0), 0.4f));

                if (shakeSequence.IsPlaying()) shakeSequence.Restart();
                else shakeSequence.Play();

                //Creates the muzzle flash
                Instantiate(muzzleFlashObject, muzzleFlashSpot);

                //Plays the shooting sound and randomizes the pitch slightly
                shotSound.pitch = Random.Range(0.95f, 1.05f);
                shotSound.Play();

                //Sets off the animation trigger
                animator.SetTrigger(Shoot);

                //Finds where the bullet lands
                if (Physics.Raycast(mainCamera.position, mainCamera.TransformDirection(Vector3.forward), out var hit, MAXHitDistance, canHit))
                {
                    //If the thing you hit happens to have a rigidbody, applies a force in the direction you're facing
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        var rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                        var direction = (transform.position - hit.transform.position).normalized;
                        rb.AddForce(-direction * knockback, ForceMode.Impulse);
                    }
                    //Creates the burst VFX
                    var vfx = Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal, Vector3.back));
                    vfx.transform.parent = hit.transform;

                    //If it hits an enemy, do damage with its script
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        //Finds an EnemyScript component in the enemy and do damage to it
                        var enemyScript = hit.transform.gameObject.GetComponentInParent<EnemyScript>();
                        enemyScript.TakeDamage(damage);

                        //Finds the collider that it hit, and plays the hit animation depending on the collider it hit
                        var hitAnimations = hit.transform.gameObject.GetComponentInParent<HitAnimations>();
                        var colliderHit = hit.transform.gameObject.GetComponent<BoxCollider>();
                        hitAnimations.BodyPartHit(colliderHit);
                    }
                }
            }


        }
        
        



    }
}
