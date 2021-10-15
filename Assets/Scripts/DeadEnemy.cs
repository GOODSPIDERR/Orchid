using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    //I will definitely have to rework this script to work for ragdolls
    Transform player;
    Rigidbody rb;
    void OnEnable()
    {
        //Getters
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        //Adds a force to the dead enemy 
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(-direction * 5f, ForceMode.Impulse);

        Death();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
