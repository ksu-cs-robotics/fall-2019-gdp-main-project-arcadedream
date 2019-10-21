using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    string input;
   

    [SerializeField] bool mouseControl;

    // Start is called before the first frame update
    //Initialization
    void Start()
    {
    }

    //Initializes each player paddle based on the player number 1-4 
    public void Init(int playerNumber)
    {
        Vector2 pos = Vector2.zero;
        switch (playerNumber)
        {
            case 1:
                // init player 1 (red)
                pos = new Vector2(PongGameManager.RedPaddleSpawn.x, PongGameManager.RedPaddleSpawn.y);
                transform.Rotate(0f, 0f, -45f);
                input = "Player1Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 2:
                // Init player 2 (blue)
                pos = new Vector2(PongGameManager.BluePaddleSpawn.x, PongGameManager.BluePaddleSpawn.y);
                transform.Rotate(0f, 0f, -45f);
                input = "Player2Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 3:
                // Init player 3 (green)
                pos = new Vector2(PongGameManager.GreenPaddleSpawn.x, PongGameManager.GreenPaddleSpawn.y);
                transform.Rotate(0f, 0f, 45f);
                input = "Player3Paddle";
                //Assign Color 
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                
                break;
            case 4:
                // Init player 4 (Yellow)
                pos = new Vector2(PongGameManager.YellowPaddleSpawn.x, PongGameManager.YellowPaddleSpawn.y);
                transform.Rotate(0f, 0f, 45f);
                input = "Player4Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;

            default:
                Debug.Log("Failed to initialize player paddle. Player " + playerNumber + " Out of range");
                break;
        }
        transform.name = input;

        //Update this paddle's position
        transform.position = pos;


    }
    // Update is called once per fixed frame
    void Update()
    {
        /*
        //Moving the PlayerPaddle
        float move = Input.GetAxis(input) * Time.deltaTime * speed; 


        //Restrict the movement of the player paddle to stay in game area
        //If playerpaddle out of LOWER bound of game area 
        if (transform.position.y < PongGameManager.bottomLeft.y + (height + 0.3f) / 2 && move < 0)
        {
            move = 0;
        }
        //If playerpaddle out of HIGHER bound of game area 
        if (transform.position.y > PongGameManager.topRight.y - (height + 0.3f) / 2 && move > 0)
        {
            move = 0;
        }

                if (mouseControl == false) //don't update position based on keys if mouse controls
        {          
            //Update the movement
            transform.Translate(move * Vector2.up);
        }

        */
        //mouse scroll changes rotation
        if (Input.GetAxis("Mouse ScrollWheel") > 0) //scroll up
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); //rotate z axis
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) //scroll down
        {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime); //rotate z axis
        }


    }


    //For mouse movement
    void OnMouseDrag()
    {
        Vector2 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 lerped = new Vector2(pos_move.x, pos_move.y);
        transform.position = Vector2.Lerp(pos_move, lerped, Time.deltaTime * 3);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GrowPowerup")
        {
           
                Vector3 scale = new Vector3(.5f, 2, 1f);
                transform.localScale = scale;
           
            
            
            Debug.Log("Player powerup Grow");
           
            Destroy(collision.gameObject);
        }
        if (collision.tag == "ShrinkPowerup")
        {

            Vector3 scale = new Vector3(.25f, 1, 1f);
            transform.localScale = scale;



            Debug.Log("Player powerup Shrink");
          
            Destroy(collision.gameObject);
        }
        if (collision.tag == "powerup")
        {
            Debug.Log("Player powerup");
            Destroy(collision.gameObject);
        }

    }

}
