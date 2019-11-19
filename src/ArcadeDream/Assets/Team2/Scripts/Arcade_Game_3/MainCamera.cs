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
        // This needs to be done dynamically, so here we search for the local player
        DefaultPlayer();
    }

    void Update()
    {
        try
        {
            targetCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        } 
        catch
        {
            // if the player no longer exists, set camera to stare at itself...
            DefaultPlayer();
        }
    }

    public void SetPlayer(GameObject newPlayer) { player = newPlayer; }
    public void DefaultPlayer() { player = this.gameObject; }
}
