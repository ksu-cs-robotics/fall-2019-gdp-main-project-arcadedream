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

        if ((player.GetComponent<Movement>().launched == true || player.transform.rotation != Quaternion.identity) && GetComponent<Camera>().orthographicSize <= 4.25f )
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 4.25f, smoothing * Time.deltaTime);
        if ((player.GetComponent<Movement>().launched == false || player.transform.rotation != Quaternion.identity) && GetComponent<Camera>().orthographicSize >= 2.5f)
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 2.5f, smoothing * Time.deltaTime);
    }
}
