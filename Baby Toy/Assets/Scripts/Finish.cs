using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public ParticleSystem particles;

    public GameObject baby;

    public GameObject EndScreen;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        particles.Play();
        baby.SetActive(false);

        Invoke("GameEnd", 2.0f);
    }

     void GameEnd()
    {
        EndScreen.SetActive(true);
    }
}
