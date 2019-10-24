using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoalieMovement : MonoBehaviour
{
    public int leftlimit;
    public int rightlimit;
    string input;
    public bool able;

    public int count;
    // Start is called before the first frame update
    void Start()
    {
        leftlimit = 0;
        rightlimit = 0;
        able = true;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (able)
        {
            if (Input.GetKeyDown(KeyCode.A) && leftlimit != 1)
            {
                this.gameObject.transform.Translate(-1, 0, 0);
                leftlimit++;
                rightlimit--;
            }

            if (Input.GetKeyDown(KeyCode.D) && rightlimit != 1)
            {
                this.gameObject.transform.Translate(1, 0, 0);
                leftlimit--;
                rightlimit++;
            }
        }

        else
        {
            count++;
            if (count == 200)
            {
                able = true;
            }
        }

      
    }


    //Initializes each player goalie based on the player number 1-4 
    public void Init(int goalieNumber)
    {
        Vector2 pos = Vector2.zero;
        Vector3 rot = Vector3.zero;
        switch (goalieNumber)
        {
            case 1:     // init player 1 (red)
                // Init pos
                pos = new Vector2(PongGameManager.RedSpawn.x, PongGameManager.RedSpawn.y);
                // Init rotation
                rot = new Vector3(0f, 0f, 45f);
                //Init Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                //Assign Name
                input = "Player1Goalie";
                gameObject.tag = "Goalie";
                break;
            case 2:     // init player 2 (blue)
                // Init pos
                pos = new Vector2(PongGameManager.BlueSpawn.x, PongGameManager.BlueSpawn.y);
                // Init rotation
                rot = new Vector3(0f, 0f, 225f);
                //Init Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                //Assign Name
                input = "Player2Goalie";
                gameObject.tag = "goalieb";
                break;
            case 3:     // init player 3 (green)
                // Init pos
                pos = new Vector2(PongGameManager.GreenSpawn.x, PongGameManager.GreenSpawn.y);
                // Init rotation
                rot = new Vector3(0f, 0f, 315f);
                //Init Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                //Assign Name
                input = "Player3Goalie";
                gameObject.tag = "goalieg";
                break;
            case 4:     // init player 4 (yellow)
                // Init pos
                pos = new Vector2(PongGameManager.YellowSpawn.x, PongGameManager.YellowSpawn.y);
                // Init rotation
                rot = new Vector3(0f, 0f, 495f);
                //Init Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                //Assign Name
                input = "Player4Goalie";
                gameObject.tag = "goaliey";
                break;
            default:
                Debug.Log("Failed to initialize player goalie. Player " + goalieNumber + " Out of range");
                break;
        }

        transform.name = input;

        //Update this paddle's position
        transform.position = pos;
        transform.Rotate(rot);
    }

}

