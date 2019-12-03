using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    //string levelToLoad = "CharacterCreation";
    string levelToLoad = "loginUI";
    public GameObject titleScreenUI;
    public GameObject settingsUI;
    public GameObject creditsUI;

    private void Start()
    {
        titleScreenUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Settings()
    {
        Debug.Log("Opening Setting");
        //make setting UI visable 
        titleScreenUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    //Settings Buttons

    public void SettingsBack()
    {
        titleScreenUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void PlayCredits()
    {
        creditsUI.SetActive(true);
    }

}
