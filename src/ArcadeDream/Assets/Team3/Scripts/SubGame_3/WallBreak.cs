using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WallBreak : MonoBehaviourPunCallbacks
{
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

    // Update is called once per framew
    void Update()
    {
        if (BrokeTimer > 0)
        {
            BrokeTimer -= Time.deltaTime;
        }

       else if(BrokeTimer < 0 && Broke == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = UnBroken;
            this.GetComponent<Collider2D>().enabled = true;
            Broke = false;
            Debug.Log(this.GetComponent<Collider2D>().enabled);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            RpcChangeSprite();
        }
    }

    [PunRPC]
    private void RpcChangeSprite()
    {
        BrokeTimer = BrokeTime;
        this.GetComponent<SpriteRenderer>().sprite = Broken;
        this.GetComponent<Collider2D>().enabled = false;
        Broke = true;
        //Debug.Log(this.GetComponent<Collider2D>().enabled);
    }
}
