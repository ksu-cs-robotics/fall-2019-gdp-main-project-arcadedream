using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    //These represent the GameObjects that hold the appropriate data. 
    public GameObject username;
    public GameObject password;
    public Button loginButton;

    //These variables will be updated automatically via the Update() function
    private string username_m;
    private string password_m;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Enables tab moving through
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            else if (password.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
        }

        //This allows one to hit enter from insode the form and have it click the button. 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (username_m != "" &&
               password_m != "")
            {
                loginButton.Select();
            }
        }

        username_m = username.GetComponent<InputField>().text;
        password_m = password.GetComponent<InputField>().text;
    }
}
