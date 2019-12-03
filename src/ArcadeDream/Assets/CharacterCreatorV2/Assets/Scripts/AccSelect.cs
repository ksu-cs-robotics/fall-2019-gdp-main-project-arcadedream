using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccSelect : MonoBehaviour
{
    public GameObject[] accarea;
    public GameObject[] acc;
    private GameObject currentAcc;
    int acccount = 0;

    public void SetButtonId(int id) //on button press sets ID
    {
        acccount = id;
        Debug.Log(acccount);
        Destroy(currentAcc);
        Destroy(currentAcc);
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

    /* old way of itterating through them all before buttons were introduced
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
    */
}

