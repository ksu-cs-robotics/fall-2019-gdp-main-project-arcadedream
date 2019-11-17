using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pipe : NetworkBehaviour
{
    //public float thrust;
    public float horizThrust;
    public float vertThrust;
    public Sprite UnBroken; //sprite for unbroken wall
    public Sprite Broken; //borken wall
    public float BrokeTime; //amouot of time wall stays broken
    public float BrokeTimer; //timer for how long it will stay broken
    public BoxCollider2D air;
    private bool Broke; //boolen so the walls not reseting every frame

    private Vector2 launchDir;
    public BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = UnBroken;
        air.enabled = false;
        Broke = false;
        launchDir = transform.up;
       // Debug.Log(transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (BrokeTimer > 0)
        {
            BrokeTimer -= Time.deltaTime;
        }

        else if (BrokeTimer < 0 && Broke == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = UnBroken;
            gameObject.layer = 9;
            air.enabled = false; 
            Broke = false;
            box.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Broke == true)
        {
            if (collision.GetComponent<Movement>().canMove == true)
            {
                
                Debug.Log(launchDir);
                collision.GetComponent<Rigidbody2D>().velocity += (launchDir  * new Vector2(horizThrust, vertThrust));
                collision.GetComponent<Movement>().launched = true;
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            RpcChangeSprite();
        }
    }

    [ClientRpc]
    private void RpcChangeSprite()
    {
        BrokeTimer = BrokeTime;
        this.GetComponent<SpriteRenderer>().sprite = Broken;
        gameObject.layer = 0;
        air.enabled = true;
        Broke = true;
        box.enabled = false;
    }
}