using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcadeButtons : MonoBehaviour
{
    public string arcadeGameSceneName;
    public Button playbutton;

    void Start()
    {
        playbutton.onClick.AddListener(delegate { PlayButton(); });
    }

    //When the play button is pressed
    public void PlayButton()
    {

        SceneManager.LoadScene(arcadeGameSceneName, LoadSceneMode.Single); //LoadSceneMode.Additive 
    }

    //When the multiplayer button is pressed
    public void PlayMultiplayerButton()
    {
        Debug.Log("Multiplayer Button");
    }

}
