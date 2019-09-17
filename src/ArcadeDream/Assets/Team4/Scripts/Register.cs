using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    //These represent the GameObjects that hold the appropriate data. 
    public GameObject name;
    public GameObject username;
    public GameObject password;
    public GameObject confirmPass;
    public Button registerButton;

    //These variables will be updated automatically via the Update() function
    private string name_m;
    private string username_m;
    private string password_m;
    private string confirmPass_m;

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
            if (name.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
            else if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            else if (password.GetComponent<InputField>().isFocused)
            {
                confirmPass.GetComponent<InputField>().Select();
            }
            else if (confirmPass.GetComponent<InputField>().isFocused)
            {
                name.GetComponent<InputField>().Select();
            }
        }

        //This allows one to hit enter from insode the form and have it click the button. 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(name_m != "" &&
               username_m != "" &&
               password_m != "" &&
               confirmPass_m != "")
            {
                registerButton.Select();
            }
        }

        name_m = name.GetComponent<InputField>().text;
        username_m = username.GetComponent<InputField>().text;
        password_m = password.GetComponent<InputField>().text;
        confirmPass_m = confirmPass.GetComponent<InputField>().text;
    }
}
