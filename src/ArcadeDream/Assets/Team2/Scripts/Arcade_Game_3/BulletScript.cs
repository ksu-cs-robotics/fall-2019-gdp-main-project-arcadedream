using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;
    Vector3 mousePos;
    public float despawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - gameObject.GetComponentInParent<Transform>().right;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = lookPos * bulletSpeed;
            
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(despawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground")
        NetworkServer.Destroy(gameObject);
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        NetworkServer.Destroy(gameObject);
    }
}
