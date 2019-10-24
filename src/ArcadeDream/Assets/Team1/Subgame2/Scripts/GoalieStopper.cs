using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieStopper : MonoBehaviour
{
 public static int holdrl;
    public static int holdrr;
    public static int holdgl;
    public static int holdgr;
    public static int holdyl;
    public static int holdyr;
    public static int holdbl;
    public static int holdbr;
    public bool change;
    public bool change1;
    public bool change2;
    public bool change3;
    // Start is called before the first frame update
    void Start()
    {
        int holdrl = 0;
        int holdrr = 0;
        int holdgl = 0;
        int holdgr = 0;
        int holdbl = 0;
        int holdbr = 0;
        int holdyl = 0;
        int holdyr = 0;
        bool change = false;
        bool change1 = false;
        bool change2 = false;
        bool change3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            temp.GetComponent<GoalieMovement>().leftlimit = 1;
            temp.GetComponent<GoalieMovement>().rightlimit = 1;
            change = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goaliey");
            temp1.GetComponent<GoalieMovement>().leftlimit = 1;
            temp1.GetComponent<GoalieMovement>().rightlimit = 1;
            GameObject temp2 = GameObject.FindGameObjectWithTag("goalieg");
            temp2.GetComponent<GoalieMovement>().leftlimit = 1;
            temp2.GetComponent<GoalieMovement>().rightlimit = 1;

        }

        if (change1)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Goalie");
            temp.GetComponent<GoalieMovement>().leftlimit = 1;
            temp.GetComponent<GoalieMovement>().rightlimit = 1;
            change1 = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goaliey");
            temp1.GetComponent<GoalieMovement>().leftlimit = 1;
            temp1.GetComponent<GoalieMovement>().rightlimit = 1;
            GameObject temp2 = GameObject.FindGameObjectWithTag("goalieg");
            temp2.GetComponent<GoalieMovement>().leftlimit = 1;
            temp2.GetComponent<GoalieMovement>().rightlimit = 1;

        }

        if (change2)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            temp.GetComponent<GoalieMovement>().leftlimit = 1;
            temp.GetComponent<GoalieMovement>().rightlimit = 1;
            change2 = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goaliey");
            temp1.GetComponent<GoalieMovement>().leftlimit = 1;
            temp1.GetComponent<GoalieMovement>().rightlimit = 1;
            GameObject temp2 = GameObject.FindGameObjectWithTag("Goalie");
            temp2.GetComponent<GoalieMovement>().leftlimit = 1;
            temp2.GetComponent<GoalieMovement>().rightlimit = 1;

        }

        if (change3)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            temp.GetComponent<GoalieMovement>().leftlimit = 1;
            temp.GetComponent<GoalieMovement>().rightlimit = 1;
            change3 = false;
            GameObject temp1 = GameObject.FindGameObjectWithTag("Goalie");
            temp1.GetComponent<GoalieMovement>().leftlimit = 1;
            temp1.GetComponent<GoalieMovement>().rightlimit = 1;
            GameObject temp2 = GameObject.FindGameObjectWithTag("goalieg");
            temp2.GetComponent<GoalieMovement>().leftlimit = 1;
            temp2.GetComponent<GoalieMovement>().rightlimit = 1;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player1Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            if (temp.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdbl = 1;
                holdbr = -1;
            }
            if (temp.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdbl = -1;
                holdbr = 1;
            }
            change = true;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goaliey");
            if (temp1.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdyl = 1;
                holdyr = -1;
            }
            if (temp1.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdyl = -1;
                holdyr = 1;
            }
            GameObject temp2 = GameObject.FindGameObjectWithTag("goalieg");
            if (temp2.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdgl = 1;
                holdgr = -1;
            }
            if (temp2.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdgl = -1;
                holdgr = 1;
            }
        }


        if (collision.name == "Player2Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Goalie");
            if (temp.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdrl = 1;
                holdrr = -1;
            }
            if (temp.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdrl = -1;
                holdrr = 1;
            }
            change1 = true;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goaliey");
            if (temp1.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdyl = 1;
                holdyr = -1;
            }
            if (temp1.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdyl = -1;
                holdyr = 1;
            }
            GameObject temp2 = GameObject.FindGameObjectWithTag("goalieg");
            if (temp2.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdgl = 1;
                holdgr = -1;
            }
            if (temp2.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdgl = -1;
                holdgr = 1;
            }
        }


        if (collision.name == "Player3Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            if (temp.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdbl = 1;
                holdbr = -1;
            }
            if (temp.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdbl = -1;
                holdbr = 1;
            }
            change2 = true;
            GameObject temp1 = GameObject.FindGameObjectWithTag("goaliey");
            if (temp1.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdyl = 1;
                holdyr = -1;
            }
            if (temp1.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdyl = -1;
                holdyr = 1;
            }
            GameObject temp2 = GameObject.FindGameObjectWithTag("Goalie");
            if (temp2.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdrl = 1;
                holdrr = -1;
            }
            if (temp2.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdrl = -1;
                holdrr = 1;
            }
        }



        if (collision.name == "Player4Paddle")
        {
            GameObject temp = GameObject.FindGameObjectWithTag("goalieb");
            if (temp.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdbl = 1;
                holdbr = -1;
            }
            if (temp.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdbl = -1;
                holdbr = 1;
            }
            change3 = true;
            GameObject temp1 = GameObject.FindGameObjectWithTag("Goalie");
            if (temp1.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdrl = 1;
                holdrr = -1;
            }
            if (temp1.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdrl = -1;
                holdrr = 1;
            }
            GameObject temp2 = GameObject.FindGameObjectWithTag("goalieg");
            if (temp2.GetComponent<GoalieMovement>().leftlimit == 1)
            {
                holdgl = 1;
                holdgr = -1;
            }
            if (temp2.GetComponent<GoalieMovement>().leftlimit == -1)
            {
                holdgl = -1;
                holdgr = 1;
            }
        }

    }
}
