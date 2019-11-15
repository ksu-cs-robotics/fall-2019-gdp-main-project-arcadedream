using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatSelect : MonoBehaviour
{
    public GameObject hatarea;
    public GameObject[] hats;
    private GameObject hat;
    int hatcount = 0;

    public void SetButtonId(int id) //on button press sets ID
    {
        hatcount = id;
        Debug.Log(hatcount);
        Destroy(hat);
        hat = Instantiate(hats[hatcount]);
        hat.transform.SetParent(hatarea.transform);
    }

    /* old way of itterating through before the buttons were introduced
    void Update()
    {
        if (Input.GetKeyDown("j")) //increment shirt count and save to loadout
        {
            Destroy(hat);
            hatcount++;
            if (hatcount >= hats.Length)
            {
                hatcount = 0;
            }
            hat = Instantiate(hats[hatcount]);
            hat.transform.SetParent(hatarea.transform);
        }

        if (Input.GetKeyDown("k")) //increment shirt count and save to loadout
        {
            Destroy(hat);
            hatcount--;
            if (hatcount < 0)
            {
                hatcount = hats.Length - 1;
            }
            hat = Instantiate(hats[hatcount]);
            hat.transform.SetParent(hatarea.transform);
        }
    }*/
}
