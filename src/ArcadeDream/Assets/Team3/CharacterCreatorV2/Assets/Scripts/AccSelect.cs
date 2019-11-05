using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccSelect : MonoBehaviour
{
    public GameObject[] accarea;
    public GameObject[] acc;
    private GameObject currentAcc;
    int acccount = 0;

    void Start()
    {
  
    }


    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            Destroy(currentAcc);
            acccount++;
            if (acccount >= acc.Length)
            {
                acccount = 0;
            }
            currentAcc = Instantiate(acc[acccount]);
            if (currentAcc.tag == "Mask")
            {
                currentAcc.transform.SetParent(accarea[0].transform);
            }
            if (currentAcc.tag == "Wrist")
            {
                currentAcc.transform.SetParent(accarea[1].transform);
            }
            if (currentAcc.tag == "Back")
            {
                currentAcc.transform.SetParent(accarea[2].transform);
            }
        }

        if (Input.GetKeyDown("m"))
        {
            Destroy(currentAcc);
            acccount--;
            if (acccount < 0)
            {
                acccount = acc.Length - 1;
            }

            currentAcc = Instantiate(acc[acccount]);
            if (currentAcc.tag == "Mask")
            {
                currentAcc.transform.SetParent(accarea[0].transform);
            }
            if (currentAcc.tag == "Wrist")
            {
                currentAcc.transform.SetParent(accarea[1].transform);
            }
            if (currentAcc.tag == "Back")
            {
                currentAcc.transform.SetParent(accarea[2].transform);
            }
        }
    }
}

