using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastPaddlePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject temp = collision.gameObject;
            temp.GetComponent<PlayerPaddleMovement>().fast = true;
        }
    }
}
