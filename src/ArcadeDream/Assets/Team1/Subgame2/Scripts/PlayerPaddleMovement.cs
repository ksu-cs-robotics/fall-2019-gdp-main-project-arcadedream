using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    float height;
    float width;

    string input;

    [SerializeField] bool mouseControl;

    // Start is called before the first frame update
    //Initialization
    void Start()
    {
        height = transform.localScale.y;
        width = transform.localScale.x;
    }

    //Initializes each player paddle based on the player number 1-4 
    public void Init(int playerNumber)
    {
        Vector2 pos = Vector2.zero;
        switch (playerNumber)
        {
            case 1:
                // init player 1 (red)
                pos = new Vector2(PongGameManager.topRight.x, 0);
                pos += Vector2.left * width; //Move a bit to the left
                input = "Player1Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 2:
                // Init player 2 (blue)
                pos = new Vector2(PongGameManager.bottomLeft.x, 0);
                pos += Vector2.right * width; //Move a bit to the right
                input = "Player2Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 3:
                // Init player 3 (green)
                pos = new Vector2(0, PongGameManager.bottomLeft.y);
                transform.Rotate(0f, 0f, 90f);
                pos += Vector2.up * width; //Move a bit up
                input = "Player3Paddle";
                //Assign Color 
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                
                break;
            case 4:
                // Init player 4 (Yellow)
                pos = new Vector2(0, PongGameManager.topLeft.y);
                transform.Rotate(0f, 0f, 90f);
                pos += Vector2.down * width; //Move a bit up
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
        if (collision.tag == "powerup")
        {
            Debug.Log("Player powerup");
            //Changes the color of the ball based on the last player to hit it last
            Destroy(collision.gameObject);
        }

    }

}
