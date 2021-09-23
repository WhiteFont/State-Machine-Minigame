using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{

    public GameObject baby;
    public GameObject dadVision;
    public GameObject momVision;

    private BoxCollider2D visionCollider1;
    private BoxCollider2D visionCollider2;
    private SpriteRenderer babySprite;
    private Color babyColor;
    public void Start()
    {
       visionCollider1 = momVision.GetComponent<BoxCollider2D>();
       visionCollider2 = dadVision.GetComponent<BoxCollider2D>();
       babySprite = baby.GetComponent<SpriteRenderer>();
       babyColor.a = 0.5f;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Baby"))
        {
            visionCollider1.enabled = false;
            visionCollider2.enabled = false;
            babySprite.color -= babyColor;
        }
        Debug.Log("hidden");

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Baby"))
        {
            visionCollider1.enabled = true;
            visionCollider2.enabled = true;
            babySprite.color += babyColor;
        }
    }
}
