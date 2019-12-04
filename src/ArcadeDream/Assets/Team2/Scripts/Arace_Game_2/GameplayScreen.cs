using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>
/// controls the countdown timer, displays player scores, responsible for ending game
/// Author: Lew Fortwangler
/// </summary>
public class GameplayScreen : MonoBehaviourPunCallbacks
{
    public GameObject gameoverUI;
    public GameObject overtimeUI;

    public Text countdownMin;
    public Text countdownSec;

    private int minutesLeft_m = 2;
    private int secondsLeft_m = 60;

    private bool tieGame_m = false;

    private Player p;
    private bool started = false;

    private void Start()
    {
        countdownMin.text = "2";
        countdownSec.text = "00";
        p = GameObject.Find("PlayerTracker").GetComponent<Player>();
        

    }

    private void Update()
    {
        if (p.start && !started)
        {
            started = true;
            StartCoroutine("Countdown");
        }



        countdownMin.text = "\n" + minutesLeft_m;

            if (secondsLeft_m == 60)
                countdownSec.text = "\n\n     00";
            else if (secondsLeft_m < 10)
                countdownSec.text = "\n\n     0" + secondsLeft_m;
            else
                countdownSec.text = "\n\n     " + secondsLeft_m;

            if (minutesLeft_m == 0 && secondsLeft_m <= 5)
            {
                tieGame_m = CheckGameTie();
            }

            if (secondsLeft_m == 0 && minutesLeft_m == 0 && tieGame_m == true)
            {
                overtimeUI.SetActive(true);
            }
            else if (secondsLeft_m == 0 && minutesLeft_m == 0 && tieGame_m == false)
            {
                gameoverUI.SetActive(true);  //activate the gameover UI
                this.enabled = false;  //deactivate this UI
                Time.timeScale = 0;
            }
        
    }

    IEnumerator Countdown()
    {
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

   

    bool CheckGameTie()
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("GoalZone");
        Debug.Log(goals[0].name + goals[1].name + goals[2].name + goals[3].name); //0=RedGoal 1=BlueGoal 2=GreenGoal 3=YellowGoal

        int max = 0;
        foreach (GameObject goal in goals) //find max points
        {
            if (goal.GetComponent<GoalLivesManager>().points > max)
            {
                max = goal.GetComponent<GoalLivesManager>().points;
            }
        }
        int nummax = 0;
        foreach (GameObject goal in goals) //if max is in more than one tie 
        {
            if (goal.GetComponent<GoalLivesManager>().points == max)
            {
                nummax++;
            }
        }
        Debug.Log(nummax + " way tie");
        if (nummax > 1)
        {
            return true;
        }
        else { return false; }

    }
}
