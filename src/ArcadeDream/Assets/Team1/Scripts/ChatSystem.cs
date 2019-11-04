using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{

    public string userName;
    public GameObject message;
    public GameObject content;
    public GameObject inputField;
    public GameObject scrollbar;

    public GameObject chatUI;
    bool active = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!active)
            {
                chatUI.SetActive(true);
                active = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape) && active)
        {
            chatUI.SetActive(false);
            active = false;
        }
    }
    public void SendChatMessage()
    {
        string str = userName + ": "+ inputField.GetComponent<InputField>().text;
        Sender(str);
    }

    private void Sender(string mes)
    {
        Instantiate(message);
        GameObject.Find("Message(Clone)").GetComponent<Text>().text = mes;
        GameObject.Find("Message(Clone)").transform.SetParent(content.transform);

        GameObject.Find("Message(Clone)").name = "Message Set"; //change the name so the next Find works
    }

    //server message
    public void NewHighscore()
    {
        string str = userName + ": " + "Server: " + userName
            + " just got a high score in [ARCADEGAMENAME] with a score of [SCORE]";
        Sender(str);

        //could display fireworks type thing in the scene
    }

    public void Scrollbar()
    {
        scrollbar.GetComponent<Scrollbar>().value = 0;
    }
}
