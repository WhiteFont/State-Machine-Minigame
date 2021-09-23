using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public ParticleSystem particles;

    public GameObject baby;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        particles.Play();
        baby.SetActive(false);
        
        //add more game ending stuff here.
    }
}
