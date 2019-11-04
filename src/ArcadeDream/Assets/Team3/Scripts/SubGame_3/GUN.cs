using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUN : MonoBehaviour
{
    public GameObject playerPos;
    public GameObject bullet;
    public static float angle;
    public GameObject bow_;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bow_move();

        if (Input.GetKeyDown(KeyCode.E))
        {
            //NETWORK HERE
            Instantiate(bullet, transform.position, playerPos.transform.rotation);
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
