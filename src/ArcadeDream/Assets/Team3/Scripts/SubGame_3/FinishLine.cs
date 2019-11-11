using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverObject;
    public Text gameOverText;

    void Start()
    {
        gameOverText.text = "Finished";
        gameOverObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("running");
            player.GetComponent<Movement>().canMove = false;
            gameOverObject.SetActive(true);
        }
    }
}
