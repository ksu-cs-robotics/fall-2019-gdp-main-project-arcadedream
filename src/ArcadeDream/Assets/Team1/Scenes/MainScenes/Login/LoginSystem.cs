using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public Text errorMessage;
    public GameObject loginUI;
    public GameObject signupUI;

    public void ChangeToLogin()
    {
        loginUI.SetActive(true);
        signupUI.SetActive(false);
    }

    public void ChangeToSignup()
    {
        loginUI.SetActive(false);
        signupUI.SetActive(true);
    }

    //with login and sign up you if you can't log in 
    //you can use the errorMessage text to appear;
    //
    //errorMessage.text = ERROR MESSAGE STRING
    //errorMessage.enable(true);


    //When the login button is pressed
    public void Login()
    {
        Debug.Log("Logging in...");
    }

    //When the signup button is pressed
    public void SignUp()
    {
        Debug.Log("Signing up...");
    }

    public void ForgotPassword()
    {
        Debug.Log("Forgot password...");
    }

    public void BackButton()
    {
        //Back to main level
        SceneManager.LoadScene("Main");

    }
}
