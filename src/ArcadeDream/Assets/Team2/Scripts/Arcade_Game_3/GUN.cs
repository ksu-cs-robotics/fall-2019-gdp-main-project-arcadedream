using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GUN : NetworkBehaviour
{
    public GameObject gun_prefab;
    GameObject gun;
    public GameObject bullet_prefab;
    GameObject bullet;
    public GameObject player_pos_prefab;
    GameObject player_pos;
    public static float angle;
    //public GameObject bow_;
    Vector3 mousePos;

    public float fireRate;
    private float timeLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            //Vector3 bork = new Vector3(2.0f, 0.0f, 0.0f);
            player_pos = Instantiate(player_pos_prefab, transform);
            NetworkServer.SpawnWithClientAuthority(player_pos,gameObject);
            gun = Instantiate(gun_prefab, player_pos.transform);//.Translate(bork));
            NetworkServer.SpawnWithClientAuthority(gun,gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isLocalPlayer)
        {
            bow_move();

            if (Input.GetKeyDown(KeyCode.E) && timeLeft <= 0)
            {
                Cmdshot();
            }
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
        }
    }

    [Command]
    private void Cmdshot()
    {
        bullet = Instantiate(bullet_prefab, gun.transform.position, gun.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(bullet,gameObject);
        timeLeft = fireRate;
    }
    
    private void bow_move() //this code was taken from https://answers.unity.com/questions/1326563/top-down-2d-game-aim-weapon-with-mouse-please-help.html
    {
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - player_pos.transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            player_pos.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }
}
