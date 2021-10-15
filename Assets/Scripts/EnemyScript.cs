using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float hp = 3f;
    public GameObject deadObject;

    public void TakeDamage(float damage) //This is run whenever damage is taken
    {
        hp -= damage;
        if (hp <= 0) //If hp reaches 0, kaput
        {
            Instantiate(deadObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
