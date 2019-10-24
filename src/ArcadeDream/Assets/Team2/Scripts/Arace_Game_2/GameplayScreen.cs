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

    public Text TRscore;  //top right player
    public Text BRscore;  //bottom right player
    public Text TLscore;  //top left player
    public Text BLscore;  //bottom left player

    public Text countdownMin;
    public Text countdownSec;

    private int minutesLeft_m = 2;
    private int secondsLeft_m = 60;

    private void Start()
    {
        TRscore.text = "Score: " + 0;
        BRscore.text = "Score: " + 0;
        TLscore.text = "Score: " + 0;
        BLscore.text = "Score: " + 0;
        countdownMin.text = "2";
        countdownSec.text = "00";

        StartCoroutine("Countdown");
    }

    private void Update()
    {
        countdownMin.text = "\n" + minutesLeft_m;

        if (secondsLeft_m == 60)
            countdownSec.text = "\n00";
        else if (secondsLeft_m < 10)
            countdownSec.text = "\n0" + secondsLeft_m;
        else
            countdownSec.text = "\n" + secondsLeft_m;

        if(secondsLeft_m == 0 && minutesLeft_m == 0) //when the time runs out
        {
            gameoverUI.SetActive(true);  //activate the gameover UI
            this.enabled = false;  //deactivate this UI
        }

        /////////////////////////////////////////
        ///code to update each player's scores///
        /////////////////////////////////////////
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
