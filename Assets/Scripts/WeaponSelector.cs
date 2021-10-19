using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This entire thing needs to be re-written
public class WeaponSelector : MonoBehaviour
{
    public GameObject[] weapons;
    [SerializeField] private int selectedWeapon = 0;
    
    private void Update()
    {
        if (Input.GetButtonDown("Weapon1"))
        {
            foreach (var w in weapons)
            {
                w.SetActive(false);
            }
            weapons[0].SetActive(true);
        }

        if (Input.GetButtonDown("Weapon2"))
        {
            foreach (var w in weapons)
            {
                w.SetActive(false);
            }
            weapons[1].SetActive(true);
        }


        if (Input.GetButtonDown("Weapon3"))
        {
            foreach (var w in weapons)
            {
                w.SetActive(false);
            }
            weapons[2].SetActive(true);
        }
    }
    
}
