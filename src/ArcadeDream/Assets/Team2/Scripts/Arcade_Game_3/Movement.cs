using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Base for movement script taken from https://github.com/mixandjam/Celeste-Movement/blob/master/Assets/Scripts/Movement.cs

public class Movement : NetworkBehaviour
{
    //Components
    private Rigidbody2D rb;
    private Collision2D coll;
    private Collision collScript;

    //Movement stats
    [SerializeField] public float speed = 5;
    [SerializeField] public float jumpForce = 10;

    [Range(0, 1)]
    public float doubleJumpModifier; //Multiplies jumpforce to decrease height of double jump
    public float slideSpeed;
    private float startingSlideSpeed;
    public float wallJumpLerp;
    public float horizontalWallJumpSpeed;

    //movement bools. temporarily public to be able to see them while testing
    public bool wallSlide = false;
    public bool hasDoubleJump = true;
    public bool pushingWall = false;
    public bool canMove = true;
    public bool wallJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collScript = GetComponent<Collision>();
        startingSlideSpeed = slideSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            //Makes walljumping way more consistant at the cost of not being able to climb walls.
            //This might be better, talk with design team
            // if (collScript.onLeftWall && x < 0) x = 0;
            // if (collScript.onRightWall && x > 0) x = 0;

            Vector2 dir = new Vector2(x, y);

            if (canMove && x != 0)
                Walk(dir);

            if ((!Input.GetAxisRaw("Jump").Equals(0)) && canMove)
                Jump(Vector2.up);

            if (collScript.grounded || wallSlide)
            {
                hasDoubleJump = true;

                wallJumped = false;
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
                slideSpeed = startingSlideSpeed;
            }
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
        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
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
}