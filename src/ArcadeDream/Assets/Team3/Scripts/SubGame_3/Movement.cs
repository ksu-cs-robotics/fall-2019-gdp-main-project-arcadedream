using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

//Base for movement script taken from https://github.com/mixandjam/Celeste-Movement/blob/master/Assets/Scripts/Movement.cs

public class Movement : MonoBehaviourPunCallbacks
{
    //Components
    public Text countdownText;
    public Text timerObject;
    public GameObject countdownObject;
    public GameObject timeObject;
    public GameObject gun;
    private Rigidbody2D rb;
    private Collision2D coll;
    private Collision collScript;
    private Vector3 respawnPosition;
    private Vector2 respawnDir;

    //Movement stats
    public float speed;
    public float jumpForce;
    [Range(0, 1)]
    public float doubleJumpModifier; //Multiplies jumpforce to decrease height of double jump
    public float slideSpeed;
    public float wallJumpLerp;
    public float horizontalWallJumpSpeed;
    public float countdownTime;
    private bool countdown = true;

    public bool launched;

    //movement bools. temporarily public to be able to see them while testing
    public bool wallSlide = false;
    public bool hasDoubleJump = true;
    public bool pushingWall = false;
    public bool canMove;
    public bool wallJumped = false;

    public GameTimer timer;
    SpriteRenderer playerSprite;
    public GameObject localUICanvas;

    //Pipe/launcher variables
    public float launchSpeed;
    public float maxLaunchSpeed;
    public float speedAtStart;

    //Respawn variables
    public float respawnThreshold;

    // Start is called before the first frame update
    void Start()
    {
        localUICanvas = GameObject.FindGameObjectWithTag("UI").gameObject;
        var timerTextTemp = localUICanvas.gameObject.transform.GetChild(0).gameObject;
        var countdownTextTemp = localUICanvas.gameObject.transform.GetChild(1).gameObject;
        
        // Set timer references, and inital values...
        timerObject = timerTextTemp.gameObject.GetComponent<Text>();
        timeObject = timerTextTemp.gameObject;
        timerObject.GetComponent<Subgame3GameTimer>().runTimer = true;

        countdownObject = countdownTextTemp.gameObject;
        countdownText = countdownTextTemp.gameObject.GetComponent<Text>();
        countdownTime = 5;

        canMove = false;
        rb = GetComponent<Rigidbody2D>();
        collScript = GetComponent<Collision>();
        timeObject.SetActive(false);
        countdownText.text = countdownTime.ToString();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

        speedAtStart = speed;
    }
    /*
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        this.gameObject.name = "LocalPlayer";
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>().SetPlayer(this.gameObject);

        // Start the local timer when the players start. This shouldnt have to be networked
        localUICanvas = GameObject.FindGameObjectWithTag("UI").gameObject;
    }
    */
    // Update is called once per frame
    void Update()
    {
        //if (PhotonView.isMine)
        {
            /*
            if (countdown)
            {
                countdownTime -= Time.deltaTime;
                countdownText.text = Mathf.Round(countdownTime).ToString();

                if (countdownTime <= 0)
                {
                    timeObject.SetActive(true);
                    countdownObject.SetActive(false);
                    canMove = true;
                    countdown = false;
                    //timer.runTimer = true;
                    //gun.GetComponent<GUN>().canShoot = true;
                }
            }
            */
            //if (!countdown) timer.runTimer = true;

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");


            if (collScript.onLeftWall && x < 0) x = 0;
            if (collScript.onRightWall && x > 0) x = 0;

            Vector2 dir = new Vector2(x, y);

            if (canMove)
                Walk(dir);

            if (Input.GetButtonDown("Jump") && canMove)
                Jump(Vector2.up);

            if (collScript.grounded || wallSlide)
            {
                hasDoubleJump = true;

                wallJumped = false;

                launched = false;
            }
            if (collScript.grounded && !collScript.onWall)
            {
                respawnPosition = gameObject.transform.position;
                respawnDir = dir;
            }
            if (collScript.onWall && !collScript.grounded && rb.velocity.y <= 0)
            {
                wallSlide = true;
                wallSlideFunc();

                StartCoroutine(increaseSlideSpeed());
            }
            else
            {
                wallSlide = false;
                if (gameObject.transform.position.y < respawnThreshold)
                    Respawn();
            }

            if (!collScript.onWall) slideSpeed = .2f;

            if (gameObject.transform.rotation != Quaternion.identity && collScript.grounded)
            {
                if (x > 0) speed = speedAtStart * 2;
                if (x < 0) speed = speedAtStart;
            }
            //else speed = speedAtStart;
        }
    }

    IEnumerator increaseSlideSpeed()
    {
        yield return new WaitForSeconds(.02f);
        //if (slideSpeed < 1)
        slideSpeed = slideSpeed * 1.03f;
        //else slideSpeed = 1;
    }

    private void Walk(Vector2 dir)
    {
        if (!wallJumped && !launched)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else if (launched) 
        {
            rb.velocity += new Vector2(dir.x * launchSpeed, 0);
           // rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            if (rb.velocity.x >= maxLaunchSpeed) rb.velocity = new Vector2(maxLaunchSpeed, rb.velocity.y);
            if (rb.velocity.x <= -(maxLaunchSpeed)) rb.velocity = new Vector2(-maxLaunchSpeed, rb.velocity.y);
        } 
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }    
    }

    private void Jump(Vector2 dir)
    {
        Vector2 jumpDir = dir * jumpForce;
        //Normal Jump
        if (collScript.grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += jumpDir;
        }
        //Double Jump
        else if (!collScript.grounded && hasDoubleJump && !collScript.onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += jumpDir * doubleJumpModifier;
            hasDoubleJump = false;
        }

        //Wall Jump
        if (!collScript.grounded && collScript.onWall)
        {
            StopCoroutine(DisableMovement(0));
            StartCoroutine(DisableMovement(.1f));

            Vector2 wallDir = collScript.onRightWall ? Vector2.left : Vector2.right;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += (jumpDir * .9f + wallDir * horizontalWallJumpSpeed);
            StartCoroutine(waitframe());
        }

    }

    IEnumerator waitframe()
    {
        yield return new WaitForSeconds(.04f);
        wallJumped = true;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }


    private void wallSlideFunc()
    {
        pushingWall = false;
        if ((rb.velocity.x > 0 && collScript.onRightWall) || (rb.velocity.x < 0 && collScript.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Respawn()
    {
        rb.velocity = Vector2.zero;
        if (respawnDir.x > 0)
            rb.position = new Vector2(respawnPosition.x - 0.35f, respawnPosition.y + .25f);
        else if (respawnDir.x < 0)
            rb.position = new Vector2(respawnPosition.x + 0.35f, respawnPosition.y + .25f);

        StartCoroutine(DisableMovement(1));
        //InvokeRepeating("flash", 0, .1f);
    }

    private void flash()
    {

        if (playerSprite.enabled = false)
        {
            playerSprite.enabled = true;
            Debug.Log(playerSprite.enabled);
        }
        else if (playerSprite.enabled = true)
            playerSprite.enabled = false;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (collScript.grounded) gameObject.transform.rotation = collision.gameObject.transform.rotation;
        }
        else gameObject.transform.rotation = Quaternion.identity;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            speed = speedAtStart;
        }
    }
}