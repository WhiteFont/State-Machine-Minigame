using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    public float xOffset = 18.2f;
    public GameObject cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.transform.position += new Vector3(xOffset, 0, 0);
    }

}
