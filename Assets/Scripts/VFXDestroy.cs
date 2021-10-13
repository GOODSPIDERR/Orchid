using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class VFXDestroy : MonoBehaviour
{
    private VisualEffect vfx;
    private bool itStarted = false;
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    void Update()
    {
        //The effect will self-destruct prematurely without this line
        if (vfx.aliveParticleCount != 0) itStarted = true;

        //If no particles are left alive, destroy the object
        if (vfx.aliveParticleCount == 0 && itStarted) Object.Destroy(gameObject);
    }
}
