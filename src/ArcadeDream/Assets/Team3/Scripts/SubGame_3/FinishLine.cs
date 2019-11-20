using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public bool hasPassed = false;
    

    void Start()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("running");

            collision.GetComponent<Movement>().canMove = false;
            hasPassed = true;


        }
    }
}
