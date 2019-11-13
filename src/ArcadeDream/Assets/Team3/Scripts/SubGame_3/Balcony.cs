﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony : MonoBehaviour
{

    public GameObject Rubble;
    public float BrokeTime; //amouot of time wall stays broken
    public float BrokeTimer; //timer for how long it will stay broken
    // Start is called before the first frame update
    void Start()
    {
        Rubble.GetComponent<SpriteRenderer>().enabled = false;
        Rubble.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }


    void Update()
    {
        if (BrokeTimer > 0)
        {
            BrokeTimer -= Time.deltaTime;
        }

        else if (BrokeTimer < 0)
        {
            Rubble.GetComponent<SpriteRenderer>().enabled = false;
            Rubble.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            Rubble.GetComponent<SpriteRenderer>().enabled = true;
            Rubble.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            BrokeTimer = BrokeTime;
        }
    }
}
