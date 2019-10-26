using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// controls the countdown timer, displays player scores, responsible for ending game
/// Author: Lew Fortwangler
/// </summary>
public class GameplayScreen : MonoBehaviour
{
    public GameObject gameoverUI;

    public Text countdownMin;
    public Text countdownSec;

    private int minutesLeft_m = 2;
    private int secondsLeft_m = 60;

    private void Start()
    {
        countdownMin.text = "2";
        countdownSec.text = "00";

        StartCoroutine("Countdown");
    }

    private void Update()
    {
        countdownMin.text = "\n" + minutesLeft_m;

        if (secondsLeft_m == 60)
            countdownSec.text = "\n\n00     ";
        else if (secondsLeft_m < 10)
            countdownSec.text = "\n\n0     " + secondsLeft_m;
        else
            countdownSec.text = "\n\n     " + secondsLeft_m;

        if (secondsLeft_m == 0 && minutesLeft_m == 0) //when the time runs out
        {
            gameoverUI.SetActive(true);  //activate the gameover UI
            this.enabled = false;  //deactivate this UI
        }
    }

    IEnumerator Countdown()
    {
        while (secondsLeft_m != 0 || minutesLeft_m != 0)
        {
            yield return new WaitForSeconds(1);
            if (secondsLeft_m == 60)
            {
                minutesLeft_m--;
            }
            secondsLeft_m--;

            if (secondsLeft_m == 0 && minutesLeft_m > 0)
            {
                secondsLeft_m = 60;
            }
        }
    }
}
