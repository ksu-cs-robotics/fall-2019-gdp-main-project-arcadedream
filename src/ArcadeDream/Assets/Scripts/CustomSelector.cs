using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSelector : MonoBehaviour
{
    public GameObject[] Custom; // custom array
    public UnityEngine.UI.Button RightButton; //the right arrow button
    public UnityEngine.UI.Button LeftButton; //the right arrow button
    private GameObject PlayerCustom; //Players Current Hat
    public bool RightClick = false; //checks to see if right button was clicked
    public bool LeftClick = false; //checks to see if left button was clicked
    public int Counter = -1; 
    void FixedUpdate()
    {
        if (RightButton != null) //checks to see if the right button was clcked
            RightButton.onClick.AddListener(() => RightClick = true );

        if (LeftButton != null) //checks to see if the left button was clcked
            LeftButton.onClick.AddListener(() => LeftClick = true);


        if (RightClick == true) //check for right button
        {
            Counter++; //counter goes up
            if (Counter >= -1 && Counter < Custom.Length) //check to see if we are in custom bounds
            {
                if (PlayerCustom != null) //check to see if playercustom is set
                {
                    Destroy(PlayerCustom); //if set make room for new object
                }

                PlayerCustom = Instantiate(Custom[Counter]); //set to current custom
                PlayerCustom.transform.SetParent(this.transform); //create current custom at parent location
                
            }

          
          RightClick = false; //reset right click to false
        }


        if (LeftClick == true) //check for left button
        {
            Counter--; //counter goes up
            if (Counter > -1 && Counter < Custom.Length) //check to see if we are in custom bounds
            {
                if (PlayerCustom != null) //check to see if playercustom is set
                {
                    Destroy(PlayerCustom); //if set make room for new object
                }

                PlayerCustom = Instantiate(Custom[Counter]); //set to current custom
                PlayerCustom.transform.SetParent(this.transform); //create current custom at parent location

            }

            LeftClick = false;
        }

        if (Counter >= Custom.Length || Counter == -1)//if we go out of bounds we restart from the beginning
        {
            Destroy(PlayerCustom);
            Counter = -1;
        }

        if (Counter < -1) // negative 1 loops it back around
        {
            Counter = Custom.Length - 1;
            Destroy(PlayerCustom);
            PlayerCustom = Instantiate(Custom[Counter]);
            PlayerCustom.transform.SetParent(this.transform); //create current custom at parent location
        }
    }
}
