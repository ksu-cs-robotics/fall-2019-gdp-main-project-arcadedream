using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeButtons : MonoBehaviour
{
    public string[] arcadeGameSceneNames;
    public Button playbutton;
    public GameObject waitingForPlayersUI;

    void Start()
    {
        //playbutton.onClick.AddListener(delegate { PlayButton(); });
    }


    ///\/\/\/\/\/\/\/\ 
    //
    // Xeonic Fleet Play Buttons
    //
    //When the play button is pressed
    public void PlayArcade1Button()
    {

        SceneManager.LoadScene(arcadeGameSceneNames[0]);
    }

    //When the multiplayer button is pressed
    public void PlayArcade1MultiplayerButton()
    {
        Debug.Log("Multiplayer Xeonic Fleet");
        //waitingForPlayersUI.SetActive(true);
    }

    ///\/\/\/\/\/\/\/\ 
    //
    // ExtremePong Play Buttons
    //
    //When the play button is pressed
    public void PlayArcade2Button()
    {

        SceneManager.LoadScene(arcadeGameSceneNames[1]);
    }

    //When the multiplayer button is pressed
    public void PlayArcade2MultiplayerButton()
    {
        Debug.Log("Multiplayer Extreme Pong");
        //waitingForPlayersUI.SetActive(true);
    }

    ///\/\/\/\/\/\/\/\ 
    //
    // BloodBorn Game Play Buttons
    //
    //When the play button is pressed
    public void PlayArcade3Button()
    {

        SceneManager.LoadScene(arcadeGameSceneNames[2]);
    }

    //When the multiplayer button is pressed
    public void PlayArcade3MultiplayerButton()
    {
        Debug.Log("Multiplayer BloodBorn");
        //waitingForPlayersUI.SetActive(true);
    }

    //When the play button is pressed
    public void PlayArcade4Button()
    {

        SceneManager.LoadScene(arcadeGameSceneNames[3]);
    }

    //When the multiplayer button is pressed
    public void PlayArcade4MultiplayerButton()
    {
        Debug.Log("Multiplayer Extreme Pong");
        //waitingForPlayersUI.SetActive(true);
    }




}
