using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPaddle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player1Paddle")
        {
            GameObject temp = GameObject.Find("Player2Paddle");
            GameObject temp1 = GameObject.Find("Player3Paddle");
            GameObject temp2 = GameObject.Find("Player4Paddle");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }

        if (collision.name == "Player2Paddle")
        {
            GameObject temp = GameObject.Find("Player1Paddle");
            GameObject temp1 = GameObject.Find("Player3Paddle");
            GameObject temp2 = GameObject.Find("Player4Paddle");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }

        if (collision.name == "Player3Paddle")
        {
            GameObject temp = GameObject.Find("Player2Paddle");
            GameObject temp1 = GameObject.Find("Player1Paddle");
            GameObject temp2 = GameObject.Find("Player4Paddle");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }

        if (collision.name == "Player4Paddle")
        {
            GameObject temp = GameObject.Find("Player2Paddle");
            GameObject temp1 = GameObject.Find("Player3Paddle");
            GameObject temp2 = GameObject.Find("Player1Paddle");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }
    }
}
