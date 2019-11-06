using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    Vector2 targetCamPos;
    public float smoothing = 5f;
    private bool movingHoriz;
    private bool movingVert;
    public float horizMovePoint;
    public float vertMovePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //if (movingHoriz)
       // {
            targetCamPos = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
       // }
        //if (movingVert)
       // {
            targetCamPos = new Vector2(transform.position.x, player.transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        //}

        /*
        if (player.transform.position.x - transform.position.x >= horizMovePoint || player.transform.position.x - transform.position.x <= -horizMovePoint)
            movingHoriz = true;
        else movingHoriz = false;

        if (player.transform.position.y - transform.position.y >= vertMovePoint || player.transform.position.y - transform.position.y <= -vertMovePoint)
            movingVert = true;
        else movingVert = false;
        */
    }
}
