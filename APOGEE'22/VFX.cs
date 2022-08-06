using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX : MonoBehaviour
{
    public VisualEffect vfx;

    public float playRate;
    public float limit;
    public float initialPlayRate;
    float timer;

    void Start()
    {
        playRate = initialPlayRate;
        vfx = GetComponent<VisualEffect>();
    }
    private void FixedUpdate()
    {
        if (playRate < limit)
        {
            timer -= Time.deltaTime;
            playRate += Mathf.Exp(timer);
            vfx.playRate = playRate;
        }
    }
}
