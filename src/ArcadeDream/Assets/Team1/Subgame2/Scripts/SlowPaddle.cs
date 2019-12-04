﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPaddle : MonoBehaviour
{
    public GameObject powerupUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "paddle1")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.FindWithTag("paddle2");
            GameObject temp1 = GameObject.FindWithTag("paddle3");
            GameObject temp2 = GameObject.FindWithTag("paddle4");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }

        if (collision.tag == "paddle2")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.FindWithTag("paddle1");
            GameObject temp1 = GameObject.FindWithTag("paddle3");
            GameObject temp2 = GameObject.FindWithTag("paddle4");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }

        if (collision.tag == "paddle3")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.FindWithTag("paddle2");
            GameObject temp1 = GameObject.FindWithTag("paddle1");
            GameObject temp2 = GameObject.FindWithTag("paddle4");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }

        if (collision.tag == "paddle4")
        {
            powerupUI.SetActive(true);
            GameObject temp = GameObject.FindWithTag("paddle2");
            GameObject temp1 = GameObject.FindWithTag("paddle3");
            GameObject temp2 = GameObject.FindWithTag("paddle1");
            temp.GetComponent<PlayerPaddleMovement>().slow = true;
            temp1.GetComponent<PlayerPaddleMovement>().slow = true;
            temp2.GetComponent<PlayerPaddleMovement>().slow = true;
        }
    }
}
