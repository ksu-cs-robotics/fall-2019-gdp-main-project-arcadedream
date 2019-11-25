using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Owner : MonoBehaviour
{
    NavMeshAgent Alfred;
    public GameObject restingSpot;
    private GameObject victim_m;

    private int sneakSpeed = 2;
    private int runSpeed = 5;

    private void Start()
    {
        Alfred = GetComponent<NavMeshAgent>();
        StartCoroutine("StealTickets");
    }

    private IEnumerator StealTickets()
    {
        yield return new WaitForSeconds(5);
        getPlayers();
    }

    private void getPlayers()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        int allPlayers_m = GameObject.FindGameObjectsWithTag("Player").Length;
        determineVictim(allPlayers_m, Players);
    }

    private void determineVictim(int length_m, GameObject[] playerList)
    {
        int pos_m = getRandomPlayer(length_m);
        victim_m = playerList[pos_m];

        if (victim_m.GetComponent<PlayerController>().getRobbableStatus() == true)
        {
            Alfred.speed = sneakSpeed;
            Alfred.SetDestination(victim_m.transform.position);
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
        if (other.gameObject.tag == "Player")
        {
            victim_m.GetComponent<Wallet>().StealTickets();
            Alfred.speed = runSpeed;
            Alfred.SetDestination(restingSpot.transform.position);
            StartCoroutine("StealTickets");
        }
    }
}