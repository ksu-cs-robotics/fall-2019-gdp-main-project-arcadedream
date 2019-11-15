using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GoalieMovement : NetworkBehaviour
{
    public int leftlimit;
    public int rightlimit;
    string input;
    public bool able;
    public Sprite red_goalie;
    public Sprite blue_goalie;
    public Sprite green_goalie;
    public Sprite yellow_goalie;

    private Transform redSpawn;
    private Transform blueSpawn;
    private Transform greenSpawn;
    private Transform yellowSpawn;

    public Player p;



    public int count;
    // Start is called before the first frame update
    void Start()
    {
        leftlimit = 0;
        rightlimit = 0;
        able = true;
        count = 0;
        p = GameObject.Find("PlayerTracker").GetComponent<Player>();
        redSpawn = GameObject.Find("1").GetComponent<Transform>();
        blueSpawn = GameObject.Find("4").GetComponent<Transform>();
        greenSpawn = GameObject.Find("3").GetComponent<Transform>();
        yellowSpawn = GameObject.Find("2").GetComponent<Transform>();

        if (this.transform.position == redSpawn.position) this.Init(1);
        else if (this.transform.position == blueSpawn.position) this.Init(2);
        else if (this.transform.position == greenSpawn.position) this.Init(3);
        else if (this.transform.position == yellowSpawn.position) this.Init(4);
    
}
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (able && isLocalPlayer && p.move_on)
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
                gameObject.GetComponent<SpriteRenderer>().sprite = red_goalie;
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
                gameObject.GetComponent<SpriteRenderer>().sprite = blue_goalie;
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
                gameObject.GetComponent<SpriteRenderer>().sprite = green_goalie;
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
                gameObject.GetComponent<SpriteRenderer>().sprite = yellow_goalie;
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
        //transform.position = pos;
        //transform.Rotate(rot);
    }
    
}

