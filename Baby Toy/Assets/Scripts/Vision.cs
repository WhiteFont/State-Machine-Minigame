using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    AI ai;
    public static bool holdingBaby = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ai = GameObject.FindGameObjectWithTag("npc").GetComponent<AI>();
        ai = this.GetComponentInParent<AI>();
        
        if (other.CompareTag("Baby") && !holdingBaby)
        {
            ai.SeeBaby();
        }
    }
}
