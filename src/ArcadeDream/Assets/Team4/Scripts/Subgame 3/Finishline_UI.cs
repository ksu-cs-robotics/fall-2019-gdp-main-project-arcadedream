using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Finishline_UI : MonoBehaviour
{
    public Text timeText;
    public GameObject endUI;
    public FinishLine endpoint;
    public SubGame3Movement player_Prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (endpoint.hasPassed) {
            endUI.SetActive(true);
            timeText.text = player_Prefab.timer.ToString();
        }
        
    }
}
