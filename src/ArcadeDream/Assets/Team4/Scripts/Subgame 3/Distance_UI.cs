using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Distance_UI : MonoBehaviour
{
    public Image distanceBar_m;
    public Text distanceText_m;
    public GameObject player_prefab;
    public GameObject endPoint;
    public GameObject beginPoint;
    private float distance_m;
    private float true_Distance_m;
    // Start is called before the first frame update
    void Start()
    {
        distanceText_m.text = "0.0 meters";
        true_Distance_m =Mathf.Abs(endPoint.transform.position.x - beginPoint.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        //This needs to be a rpc command to tell the clients that their object is moving to accurately show the distance bar
        //Debug.Log(player_prefab.transform.position.x);
        distance_m = Mathf.Abs(player_prefab.transform.position.x - beginPoint.transform.position.x);
        //Debug.Log(distance_m);
        distanceBar_m.fillAmount = distance_m / true_Distance_m;
        //Debug.Log(distance_m / true_Distance_m);
        distanceText_m.text = distance_m.ToString("F1") + " meters";
        
    }
}
