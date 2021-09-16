using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarneBabyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(5 * -Time.deltaTime, 0, 0);
        }
    }
}
