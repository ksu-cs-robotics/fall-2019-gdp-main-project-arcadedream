using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text countdownText;
    public float countdown = 20f;
    float currentTime = 0f;

    void Start()
    {
        currentTime = countdown;
    }

    void Update()
    {
        if (currentTime >= 0)
        {
            countdownText.color = Color.white;
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            //Timer turns red when under 5 seconds left
            if (currentTime <= 5f) { countdownText.color = Color.red; }
        }

        if (currentTime <= 0)
        {
            //play game over sound
            //gameOverUI.SetActive(true);

            
        }
    }
}
