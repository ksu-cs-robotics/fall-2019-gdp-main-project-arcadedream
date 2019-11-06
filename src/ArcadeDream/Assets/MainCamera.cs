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
            targetCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
      
    }
}
