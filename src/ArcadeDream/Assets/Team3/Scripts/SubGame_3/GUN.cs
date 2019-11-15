using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GUN : NetworkBehaviour
{
    public GameObject playerPos;
    public GameObject bullet;
    public static float angle;
    public bool canShoot = false;
    Vector3 mousePos;

    public float fireRate;
    private float timeLeft = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bow_move();

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")) && timeLeft <= 0)
        {
            //NETWORK HERE
            if (canShoot)
            {
                Instantiate(bullet, transform.position, playerPos.transform.rotation);
                NetworkServer.Spawn(bullet);
                timeLeft = fireRate;
            }
        }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }

    }

    private void bow_move() //this code was taken from https://answers.unity.com/questions/1326563/top-down-2d-game-aim-weapon-with-mouse-please-help.html
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - playerPos.transform.position;
        angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        playerPos.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
