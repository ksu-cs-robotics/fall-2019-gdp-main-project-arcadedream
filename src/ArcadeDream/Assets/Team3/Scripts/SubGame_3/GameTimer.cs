using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    public bool runTimer;

    private double timer = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        //runTimer = false;
        timerText.text = timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (runTimer)
        {
            timer += Time.deltaTime;
            timerText.text = System.Math.Round(timer, 2).ToString();
        }
    }
}
