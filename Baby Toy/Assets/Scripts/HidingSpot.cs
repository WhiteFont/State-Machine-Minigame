using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{

    public GameObject baby;
    public GameObject vision;

    BoxCollider2D visionCollider;

    public void Start()
    {
       visionCollider = vision.GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Baby")
        {
            visionCollider.enabled = false;
        }
        Debug.Log("hidden");

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Baby")
        {
            visionCollider.enabled = true;
        }
    }
}
