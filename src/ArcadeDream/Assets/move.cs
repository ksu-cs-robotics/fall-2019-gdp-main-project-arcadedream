using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    float speed =5;
    private float moves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) {
            transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * speed;
        }

    }
}
