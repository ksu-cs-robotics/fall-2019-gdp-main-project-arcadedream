using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject HUDUI;
    public GameObject chatUI;

    //temporary fix for player loading into next scene
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && chatUI.activeSelf == false)
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HUDUI.SetActive(true);
        gamePaused = false;
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        HUDUI.SetActive(false);
        gamePaused = true;
    }
    
    public void MainMenu()
    {
        Debug.Log("menu");
        SceneManager.LoadScene("TitleScreen");
    }

    public void JoinLobby()
    {
        SceneManager.LoadScene("loginUI");
    }

    public void CreateLobby()
    {
        SceneManager.LoadScene("loginUI");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
