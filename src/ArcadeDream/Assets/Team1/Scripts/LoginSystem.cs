using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoginSystem : MonoBehaviour
{
    public GameObject launcherObject;
    public GameObject errorMessage;
    public GameObject loginUI;
    public GameObject signupUI;

    private Text errorText;

    private void Start()
    {
        errorText = errorMessage.GetComponent<Text>();
    }

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

    //When the login button is pressed
    public void Login()
    {
        Debug.Log("Logging in...");
        string username = loginUI.transform.GetChild(1).GetComponent<InputField>().text;
        string password = loginUI.transform.GetChild(2).GetComponent<InputField>().text;

        if (username != "" && password != "")
            StartCoroutine(SendLogin(username, password));
        else
        {
            errorText.text = "Enter username and password";
            errorMessage.SetActive(true);
        }
    }

    //When the signup button is pressed
    public void SignUp()
    {
        Debug.Log("Signing up...");
        string username = signupUI.transform.GetChild(1).GetComponent<InputField>().text;
        string password = signupUI.transform.GetChild(2).GetComponent<InputField>().text;
        string confirmPassword = signupUI.transform.GetChild(3).GetComponent<InputField>().text;

        if (username != "" && password != "" && confirmPassword != "")
        {
            if (password != confirmPassword)
            {
                errorText.text = "Passwords do not match";
                errorMessage.SetActive(true);
            }
            else
                StartCoroutine(register(username, password));
        }
        else
        {
            errorText.text = "Complete all fields";
            errorMessage.SetActive(true);
        }
    }

    public void ForgotPassword()
    {
        Debug.Log("Forgot password...");
    }

    public void BackButton()
    {
        //Back to main level
        SceneManager.LoadScene("TitleScreen");

    }

    public void CreatorCharacterButton()
    {
        //Back to main level
        SceneManager.LoadScene("CharacterCreation");

    }

    private IEnumerator SendLogin(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://131.123.42.251/Unity/DBLogin.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) Debug.Log("Error: " + www.error);
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log("Return: " + data);
                byte[] results = www.downloadHandler.data;

                if (data != "Login Successful")
                {
                    errorText.text = data;
                    errorMessage.SetActive(true);
                }
                else
                {
                    errorText.text = "Logging in...";
                    errorMessage.SetActive(true);
                    launcherObject.GetComponent<Launcher>().Connect();
                }
            }
        }
    }

    public IEnumerator register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://131.123.42.251/Unity/DBregister.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) Debug.Log("Error: " + www.error);
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log("Return: " + data);
                byte[] results = www.downloadHandler.data;

                if (data != "New record created")
                {
                    errorText.text = data;
                    errorMessage.SetActive(true);
                }
                else
                {
                    errorText.text = "Account Created";
                    errorMessage.SetActive(true);
                    ChangeToLogin();
                }
            }
        }
    }
}
