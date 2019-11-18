using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastPaddlePowerUp : MonoBehaviour
{
    public GameObject powerupUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            powerupUI.SetActive(true);
            GameObject temp = collision.gameObject;
            temp.GetComponent<PlayerPaddleMovement>().fast = true;
        }
    }
}
