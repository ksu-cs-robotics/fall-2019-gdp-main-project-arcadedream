using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSelect : MonoBehaviour
{
    public GameObject hatarea;
    public GameObject[] hats;
    private GameObject hat;
    int hatcount = 0;
    void Start()
    {
      
    }


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
    }
}
