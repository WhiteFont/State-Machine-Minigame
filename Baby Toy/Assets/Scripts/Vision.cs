using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    AI ai;
    private void OnTriggerEnter2D(Collider2D other)
    {
        ai = GameObject.FindGameObjectWithTag("npc").GetComponent<AI>();

        if (other.tag == "Baby")
        {
            ai.SeeBaby();
        }
    }
}
