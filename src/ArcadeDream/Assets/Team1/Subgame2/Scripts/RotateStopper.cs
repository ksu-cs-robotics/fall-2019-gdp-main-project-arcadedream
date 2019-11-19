using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStopper : MonoBehaviour
{
    public GameObject powerupUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player1Paddle")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.Find("Player2Paddle");
            GameObject temp1 = GameObject.Find("Player3Paddle");
            GameObject temp2 = GameObject.Find("Player4Paddle");
            temp.GetComponent<PlayerPaddleMovement>().checker = false;
            temp1.GetComponent<PlayerPaddleMovement>().checker = false;
            temp2.GetComponent<PlayerPaddleMovement>().checker = false;
        }

        if (collision.name == "Player2Paddle")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.Find("Player1Paddle");
            GameObject temp1 = GameObject.Find("Player3Paddle");
            GameObject temp2 = GameObject.Find("Player4Paddle");
            temp.GetComponent<PlayerPaddleMovement>().checker = false;
            temp1.GetComponent<PlayerPaddleMovement>().checker = false;
            temp2.GetComponent<PlayerPaddleMovement>().checker = false;
        }

        if (collision.name == "Player3Paddle")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.Find("Player2Paddle");
            GameObject temp1 = GameObject.Find("Player1Paddle");
            GameObject temp2 = GameObject.Find("Player4Paddle");
            temp.GetComponent<PlayerPaddleMovement>().checker = false;
            temp1.GetComponent<PlayerPaddleMovement>().checker = false;
            temp2.GetComponent<PlayerPaddleMovement>().checker = false;
        }

        if (collision.name == "Player4Paddle")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.Find("Player2Paddle");
            GameObject temp1 = GameObject.Find("Player3Paddle");
            GameObject temp2 = GameObject.Find("Player1Paddle");
            temp.GetComponent<PlayerPaddleMovement>().checker = false;
            temp1.GetComponent<PlayerPaddleMovement>().checker = false;
            temp2.GetComponent<PlayerPaddleMovement>().checker = false;
        }
    }
}
