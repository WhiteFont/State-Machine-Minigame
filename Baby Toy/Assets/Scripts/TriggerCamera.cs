using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    public float xOffset = 18.2f;
    public GameObject cam;
    private bool flip = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flip)
        {
            cam.transform.position += new Vector3(xOffset, 0, 0);
            gameObject.transform.position += new Vector3(-3, 0, 0);
            flip = true;
        }
        else if (flip)
        {
            cam.transform.position += new Vector3(-xOffset, 0, 0);
            gameObject.transform.position += new Vector3(3, 0, 0);
            flip = false;
        }
    }

}
