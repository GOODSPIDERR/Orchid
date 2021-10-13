using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float hp = 3f;
    public GameObject deadObject;
    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(float damage) //This is run whenever damage is taken
    {
        hp -= damage;
        if (hp <= 0)
        {
            Instantiate(deadObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
