using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject player;
    public Text selectionText;
    int selectionPanel = 0;

    //Buttons
    GameObject back;
    GameObject next;
    GameObject continueButton;

    //Panels
    public GameObject panel0;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;

    void Start()
    {
        //Find buttons
        back = GameObject.Find("SelectionBack");
        next = GameObject.Find("SelectionNext");
        continueButton = GameObject.Find("SelectionContinue");

    }

    void Update()
    {

        switch (selectionPanel)
        {
            case 0:
                selectionText.text = "SELECT SKIN COLOR";
                //disable back button
                back.SetActive(false);
                continueButton.SetActive(false);
                //Panels
                panel0.SetActive(true);
                panel1.SetActive(false);
                break;
            case 1:
                selectionText.text = "SELECT CLOTHES";
                //enable back button
                back.SetActive(true);
                //Panels
                panel0.SetActive(false);
                panel1.SetActive(true);
                panel2.SetActive(false);
                break;
            case 2:
                selectionText.text = "SELECT EYES";
                //Panels
                panel1.SetActive(false);
                panel2.SetActive(true);
                panel3.SetActive(false);
                break;
            case 3:
                selectionText.text = "SELECT HAT";
                panel2.SetActive(false);
                panel3.SetActive(true);
                panel4.SetActive(false);
                continueButton.SetActive(false);
                break;
            case 4:
                selectionText.text = "SELECT ACCESSORY";
                //enable next button
                next.SetActive(false);
                //disable continue button
                continueButton.SetActive(true);
                panel3.SetActive(false);
                panel4.SetActive(true);
                break;     
            default:
                Debug.Log("Out of range");
                break;
        }

    }

    //Buttons

    //Pressing the back button in the top left of the scene will take you back to the previous scene
    public void BackButton()
    {
        int previousLevel = PlayerPrefs.GetInt("previousLevel");
        Destroy(player);
        SceneManager.LoadScene(previousLevel);
    }

    public void NextPanelButton()
    {
        selectionPanel++;
    }

    public void BackPanelButton()
    {
        selectionPanel--;
    }

    public void RotatePlayerClockwise()
    {
        player.transform.Rotate(Vector3.up * 20);

    }

    public void RotatePlayerCounterClockwise()
    {
        player.transform.Rotate(Vector3.down * 20);
    }

    public void ContinueButton()
    {
        //save all the character choices

        //go to main level
        Destroy(player);
        SceneManager.LoadScene("Main");

    }

}
