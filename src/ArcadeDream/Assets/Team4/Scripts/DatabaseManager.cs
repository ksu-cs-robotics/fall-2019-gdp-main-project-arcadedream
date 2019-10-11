using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public IEnumerator updatePlayer(int userID, string updateType, int amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("updateType", updateType);
        form.AddField("amount", amount);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity/DBUpdate.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) Debug.Log("Error: " + www.error);
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log("Return: " + data);
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator updateScore(int userID, string updateType, int gameTime, int score, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("updateType", updateType);
        form.AddField("gameTime", gameTime);
        form.AddField("highscore", score);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity/DBUpdate.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) Debug.Log("Error: " + www.error);
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log("Return: " + data);
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}
