using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject tieGameUI;
    public Text countdownText;
    public float countdown = 20f;
    float currentTime = 0f;
    bool gameTie = true;

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
            gameTie = CheckGameTie();
            if (gameTie) { OverTime(); }
            //PLAY GAME OVER SOUND
            else
            {
                tieGameUI.SetActive(false);
                gameOverUI.SetActive(true);
                Time.timeScale = 0;
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
        int nummax= 0;
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

    void OverTime()
    {
        tieGameUI.SetActive(true); //activate tie game UI
    }
}
