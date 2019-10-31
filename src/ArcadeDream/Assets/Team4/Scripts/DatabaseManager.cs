using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//10/20/19

//author: Connor DeGeorge

//version: 1.0

public class DatabaseManager : MonoBehaviour, IDisposable
{
    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    ///  Used to update different data of the player and push it to the database. Possible types include: coins, points, equipt, and perms
    /// </summary>
    /// <param name="userID"> The unique user ID of the player </param>
    /// <param name="updateType"> What needs to be updated. Possible types include: coins, points, equipt and perms </param>
    /// <param name="amount"> The value that will be pushed to the database. THIS IS REPLACE THE CURRENT VALUE. </param>
    public IEnumerator updatePlayer(int userID, string updateType, int amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("updateType", updateType);
        form.AddField("amount", amount);

        using (UnityWebRequest www = UnityWebRequest.Post("http://131.123.42.251/Unity/DBUpdate.php", form))
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

    /// <summary>
    /// Used to update subgame score data and push it to the database. Possible updateTypes include: game1, game2, game3, and game4
    /// </summary>
    /// <param name="userID"> The unique ID of the user </param>
    /// <param name="updateType"> What needs to be updated. Possible types include: game1, game2, game3, and game4  </param>
    /// <param name="gameTime"> The time stamp of the end of the game </param>
    /// <param name="score"> The score the player received </param>
    /// <param name="username"> The username of the player that will be displayed on the leaderboards </param>
    /// <returns></returns>
    public IEnumerator updateScore(int userID, string updateType, int gameTime, int score, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("updateType", updateType);
        form.AddField("gameTime", gameTime);
        form.AddField("highscore", score);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://131.123.42.251/Unity/DBUpdate.php", form))
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

    private IEnumerator getPlayerFromDatabase(int userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        Debug.Log("running");
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://131.123.42.251/Unity/DBPull.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) Debug.Log("Error: " + www.error);
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log("Return: " + data);

                callback(data);
            }
        }
    }

    public PlayerObject getPlayer(int userID)
    {
        PlayerObject playerObject = null;
        StartCoroutine(getPlayerFromDatabase(userID, (returnvalue) => {
            playerObject = JsonUtility.FromJson<PlayerObject>(returnvalue);
        }));
        return playerObject;
    }

    public void Dispose() { /* As there is no instance specific data yet, there is nothing to dispose of! */ }
}