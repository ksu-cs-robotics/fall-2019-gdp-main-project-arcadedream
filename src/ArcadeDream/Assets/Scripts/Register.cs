using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    //These represent the GameObjects that hold the appropriate data.
    public GameObject username;
    public GameObject password;
    public GameObject confirmPass;
    public Button registerButton;

    //These variables will be updated automatically via the Update() function
    private string username_m;
    private string password_m;
    private string confirmPass_m;
    private DatabaseManager DatabaseManager;

    void Start()
    {
        registerButton.onClick.AddListener(() =>
        {
            StartCoroutine(register(username_m, password_m));
        });
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
                confirmPass.GetComponent<InputField>().Select();
            }
        }

        //This allows one to hit enter from insode the form and have it click the button. 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(username_m != "" &&
               password_m != "" &&
               confirmPass_m != "")
            {
                StartCoroutine(register(username_m, password_m));
            }
        }
        
        username_m = username.GetComponent<InputField>().text;
        password_m = password.GetComponent<InputField>().text;
        confirmPass_m = confirmPass.GetComponent<InputField>().text;
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
            }
        }
    }
}
