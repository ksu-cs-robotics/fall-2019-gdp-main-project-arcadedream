using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleGrowPowerup : MonoBehaviour
{
    GameObject paddleToGrow;

    void Start(){

        paddleToGrow = GameObject.Find("PlayerPaddle");        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter (Collision col)
    {
        Vector3 scale = new Vector3(2,2, 1f);                                    
        if(col.gameObject.name == "PlayerPaddle")
        {
             paddleToGrow.gameObject.transform.localScale = scale;
        }
     }
}
