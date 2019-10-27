using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    public GameObject growUI;
    public GameObject shrinkUI;

    public Sprite red;
    public Sprite blue;
    public Sprite green;
    public Sprite yellow;

    string input;
    bool waiting = false;
    private IEnumerator coroutine;
    private IEnumerator coroutine2;
    public int speedtime;
    [SerializeField] bool mouseControl;

    public int count;
    public int counter;
    public bool checker;

    public bool slow;
    public bool fast;

    // Start is called before the first frame update
    //Initialization
    void Start()
    {
        checker = true;
        slow = false;
        fast = false;
        counter = 0;
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
                gameObject.GetComponent<SpriteRenderer>().sprite = red;
                break;
            case 2:
                // Init player 2 (blue)
                pos = new Vector2(PongGameManager.BluePaddleSpawn.x, PongGameManager.BluePaddleSpawn.y);
                transform.Rotate(0f, 0f, -45f);
                input = "Player2Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().sprite = blue;
                break;
            case 3:
                // Init player 3 (green)
                pos = new Vector2(PongGameManager.GreenPaddleSpawn.x, PongGameManager.GreenPaddleSpawn.y);
                transform.Rotate(0f, 0f, 45f);
                input = "Player3Paddle";
                //Assign Color 
                gameObject.GetComponent<SpriteRenderer>().sprite = green;

                break;
            case 4:
                // Init player 4 (Yellow)
                pos = new Vector2(PongGameManager.YellowPaddleSpawn.x, PongGameManager.YellowPaddleSpawn.y);
                transform.Rotate(0f, 0f, 45f);
                input = "Player4Paddle";
                //Assign Color
                gameObject.GetComponent<SpriteRenderer>().sprite = yellow;
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
        //mouse scroll changes rotation
        if (checker)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) //scroll up
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); //rotate z axis
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0) //scroll down
            {
                transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime); //rotate z axis
            }
        }
        else
        {
            counter++;
            if (counter == 300)
            {
                checker = true;
                counter = 0;
            }
        }


    }



    //For mouse movement
    void OnMouseDrag()
    {
        Vector2 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 lerped = new Vector2(pos_move.x, pos_move.y);
        if (fast)
        {
            if (speedtime != 500)
            {
                speedtime++;
                transform.position = Vector2.Lerp(pos_move, lerped, Time.deltaTime * 3);
            }
            if (speedtime == 500)
            {
                speedtime = 0;
                fast = false;
            }
        }

        if (slow)
        {
            if (speedtime != 500)
            {
                speedtime++;
                count++;
                if (count == 20)
                {
                    transform.position = Vector2.Lerp(pos_move, lerped, Time.deltaTime * 3);
                    count = 0;
                }
            }
            if (speedtime == 500)
            {
                speedtime = 0;
                slow = false;
            }
        }

        if (!fast && !slow)
        {
            count++;
            if (count == 10)
            {
                transform.position = Vector2.Lerp(pos_move, lerped, Time.deltaTime * 3);
                count = 0;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GrowPowerup")
        {
            growUI.SetActive(true);
                Vector3 scale = new Vector3(.5f, 2, 1f);
                transform.localScale = scale;

                coroutine2 = WaitandScale2(3f);
                StartCoroutine(coroutine2);
            
            Debug.Log("Player powerup Grow");
           
            Destroy(collision.gameObject);
        }
        if (collision.tag == "ShrinkPowerup")
        {
            shrinkUI.SetActive(true);
            Vector3 scale = new Vector3(.25f, 1, 1f);
            Vector3 scale2 = new Vector3(.5f, 1.5f, 1f);
            GameObject[] paddles;
            paddles = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject paddler in paddles)
            {
                paddler.transform.localScale = scale;

                coroutine = WaitandScale(3f);
                StartCoroutine(coroutine);

            }
            

           
            transform.localScale = scale2;



            Debug.Log("Player powerup Shrink");
          
            Destroy(collision.gameObject);
        }
        if (collision.tag == "powerup")
        {
            Debug.Log("Player powerup");
            Destroy(collision.gameObject);
        }

    }
    IEnumerator WaitandScale2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 scale2 = new Vector3(.5f, 1.5f, 1f);
        transform.localScale = scale2;
    }
    
     IEnumerator WaitandScale(float waitTime)
    {
      //  waiting = true;
        yield return new WaitForSeconds(waitTime);

        Vector3 scale2 = new Vector3(.5f, 1.5f, 1f);
        GameObject[] paddles;
        paddles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject paddler in paddles)
        {
            // paddler.transform.localScale = scale;

            paddler.transform.localScale = scale2;

        }
      //  waiting = false;

    }
}
