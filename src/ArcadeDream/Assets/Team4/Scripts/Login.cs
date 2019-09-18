using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine(login(username_m, password_m));
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
                username.GetComponent<InputField>().Select();
            }
        }

        //This allows one to hit enter from insode the form and have it click the button. 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (username_m != "" &&
               password_m != "")
            {
                StartCoroutine(login(username_m, password_m));
            }
        }

        username_m = username.GetComponent<InputField>().text;
        password_m = password.GetComponent<InputField>().text;
    }

    public IEnumerator login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity/DBLogin.php", form))
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
