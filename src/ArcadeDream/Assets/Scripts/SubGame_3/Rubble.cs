using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour
{
    public float slowdown;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Movement>().speed = collision.GetComponent<Movement>().speed/slowdown;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Movement>().speed = collision.GetComponent<Movement>().speed * slowdown;
        }
    }
}
