﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomBall : MonoBehaviour
{
    public GameObject powerupUI;

    public GameObject phantoms;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            powerupUI.SetActive(true);
            Instantiate(phantoms, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(phantoms, new Vector3(0, 0, 0), Quaternion.identity);
            coroutine = Waitandkill(3f);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator Waitandkill(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        GameObject[] phantomballs = GameObject.FindGameObjectsWithTag("PhantomBall");
        for (var i = 0; i < phantomballs.Length; i++ )
        {
            Destroy (phantomballs[i]);

        }

    }
}
