using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    public string levelToLoad = "Main";


    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Settings()
    {
        Debug.Log("Opening Setting");
        //
        //make setting UI visable 
        //
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }


}
