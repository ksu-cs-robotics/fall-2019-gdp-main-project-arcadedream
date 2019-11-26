using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CarGame
{
    public class RaceCountdown : MonoBehaviour
    {
        public float laptime;
        public Text timerText;
        public int minute;
        [HideInInspector] public bool gameFinished;
        public Text finishedTimeText;

        string highscore;

        public float countdown = 3.49f;
        public float currentTime = 0f;
        void Start()
        {
            currentTime = countdown;
            laptime = 0f;
            gameFinished = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
                this.GetComponent<Text>().text = currentTime.ToString("0");

                //if (currentTime <= 1f) { this.GetComponent<Text>().color = Color.red; }

            }

            if (currentTime <= 0.5f)
            {
                this.GetComponent<Text>().text = "GO!";
                currentTime -= 1 * Time.deltaTime;
                PlayerCar.isPlaying = true;
                if (currentTime <= -2f)
                {
                    this.GetComponent<Text>().text = "";
                }
            }

            //Start timer
            if (currentTime < 0f && !gameFinished)
            {
                laptime += 1 * Time.deltaTime;
                if (laptime > 60f)
                {
                    minute++;
                    laptime = 0f;
                }
                timerText.text = minute + ":" + laptime.ToString("00.00");

            }
        }

        public void FinishedTime()
        {
            finishedTimeText.text = "Time: " + minute + ":" + laptime.ToString("00.00");
        }
    }
}