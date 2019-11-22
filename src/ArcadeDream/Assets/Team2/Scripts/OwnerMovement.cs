using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OwnerMovement : MonoBehaviour
{
    NavMeshAgent Owner;
    public GameObject restingSpot;

    private int sneakSpeed = 2;
    private int runSpeed = 5;

    private void Start()
    {
        Owner = GetComponent<NavMeshAgent>();
        StartCoroutine("StealTickets");
    }
    
    private IEnumerator StealTickets()
    {
        yield return new WaitForSeconds(5);
        getPlayers();
    }
    
    private void getPlayers()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("ArcadePlayer");
        int allPlayers_m = GameObject.FindGameObjectsWithTag("ArcadePlayer").Length;
        determineVictim(allPlayers_m, Players); 
    }

    private void determineVictim(int length_m, GameObject[] playerList)
    {
        int pos_m = getRandomPlayer(length_m);

        if(playerList[pos_m].GetComponent<Player_ADTest>().getRobbableStatus() == true)
        {
            Owner.speed = sneakSpeed;
            Owner.SetDestination(playerList[pos_m].transform.position);
        }
        else
        {
            determineVictim(length_m, playerList);
        }
    }

    private int getRandomPlayer(int num_m)
    {
        int rand_m = Random.Range(0, num_m);
        return rand_m;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ArcadePlayer")
        {
            //INSERT CODE TO STEAL FROM PLAYER
            Owner.speed = runSpeed;
            Owner.SetDestination(restingSpot.transform.position);
            StartCoroutine("StealTickets");
        }
    }
}