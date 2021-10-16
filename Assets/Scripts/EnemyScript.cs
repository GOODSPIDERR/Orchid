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
        if (!(hp <= 0)) return;
        var transform1 = transform;
        Instantiate(deadObject, transform1.position, transform1.rotation);
        Destroy(gameObject);
    }
}
