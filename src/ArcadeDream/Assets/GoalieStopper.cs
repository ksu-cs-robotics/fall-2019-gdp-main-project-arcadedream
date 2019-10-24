using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieStopper : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player1Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            temp.GetComponent<GoalieMovement>().able = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goalieg");
            temp1.GetComponent<GoalieMovement>().able = false;
            GameObject temp2 = GameObject.FindGameObjectWithTag("goaliey");
            temp2.GetComponent<GoalieMovement>().able = false;

        }

        if (collision.name == "Player2Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Goalie");
            temp.GetComponent<GoalieMovement>().able = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goalieg");
            temp1.GetComponent<GoalieMovement>().able = false;
            GameObject temp2 = GameObject.FindGameObjectWithTag("goaliey");
            temp2.GetComponent<GoalieMovement>().able = false;

        }

        if (collision.name == "Player3Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            temp.GetComponent<GoalieMovement>().able = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("Goalie");
            temp1.GetComponent<GoalieMovement>().able = false;
            GameObject temp2 = GameObject.FindGameObjectWithTag("goaliey");
            temp2.GetComponent<GoalieMovement>().able = false;

        }

        if (collision.name == "Player4Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            temp.GetComponent<GoalieMovement>().able = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goalieg");
            temp1.GetComponent<GoalieMovement>().able = false;
            GameObject temp2 = GameObject.FindGameObjectWithTag("Goalie");
            temp2.GetComponent<GoalieMovement>().able = false;

        }

    }
}
