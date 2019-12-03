﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GUN : MonoBehaviourPunCallbacks
{
    public GameObject bullet;
    public bool canShoot = true;
    Vector3 mousePos;
    Vector2 pos;

    public float fireRate;
    private float timeLeft = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("SG3Shoot")) && timeLeft <= 0 && canShoot && photonView.IsMine) 
        {
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            CmdShootGun(angle, pos);
            timeLeft = fireRate;

        }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    [PunRPC]
    void CmdShootGun(float angle, Vector2 pos)
    {
        Debug.Log("SERVER SHOOT");
        //GameObject bullet_temp = Instantiate(bullet, pos, Quaternion.Euler(0, 0, angle));
        if (photonView.IsMine){
            PhotonNetwork.Instantiate(bullet.name, pos, Quaternion.Euler(0, 0, angle), 0);
        }
    }

}
