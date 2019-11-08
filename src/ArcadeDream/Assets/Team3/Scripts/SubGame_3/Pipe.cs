using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float thrust;
    public Sprite UnBroken; //sprite for unbroken wall
    public Sprite Broken; //borken wall
    public float BrokeTime; //amouot of time wall stays broken
    public float BrokeTimer; //timer for how long it will stay broken
    private bool Broke; //boolen so the walls not reseting every frame

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = UnBroken;
        Broke = false;
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
            Broke = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Broke == true)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 1f * thrust, 0f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            BrokeTimer = BrokeTime;
            this.GetComponent<SpriteRenderer>().sprite = Broken;
            Broke = true;
        }
    }
}