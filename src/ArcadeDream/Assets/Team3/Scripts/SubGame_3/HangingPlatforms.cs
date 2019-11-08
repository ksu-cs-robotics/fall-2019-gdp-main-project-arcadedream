using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingPlatforms : MonoBehaviour
{
    public float BrokeTime; //amouot of time wall stays broken
    public float BrokeTimer; //timer for how long it will stay broken
    private bool Broke; //boolen so the walls not reseting every frame
    private Quaternion Horizantal;
    private Quaternion Vertical;
    // Start is called before the first frame update
    void Start()
    {
        Horizantal = new Quaternion(0, 0, 90, 0);
        Vertical = new Quaternion(0, 0, 0, 0);

    }

    // Update is called once per framew
    void Update()
    {
        if (BrokeTimer > 0)
        {
            BrokeTimer -= Time.deltaTime;
        }

        else if (BrokeTimer < 0 && Broke == true)
        {
            this.transform.Rotate(0, 0, 270);
            Broke = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet" && Broke == false)
        {
            BrokeTimer = BrokeTime;
            this.transform.Rotate(0, 0, 90);
            Broke = true;
        }
    }
}
