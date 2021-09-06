using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public ParticleSystem particles;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        particles.Play();
    }
}
