using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony : MonoBehaviour
{

    public GameObject Rubble;
    // Start is called before the first frame update
    void Start()
    {
        //nothin for now
    }


    void Update()
    {
        //nothin for now
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            Rubble.GetComponent<SpriteRenderer>().enabled = true;
            Rubble.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
