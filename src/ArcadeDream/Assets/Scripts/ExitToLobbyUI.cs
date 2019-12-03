using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToLobbyUI : MonoBehaviour
{
    public GameObject exitLobbyUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(exitLobbyUI.activeSelf == false)
            {
                exitLobbyUI.SetActive(true);
            }
            else
            {
                exitLobbyUI.SetActive(false);
            }
        }
    }

    public void ResumeButton()
    {
        exitLobbyUI.SetActive(false);
    }

    public void GoToMainLobbyButton()
    {
        SceneManager.LoadScene("Main");
    }
}
