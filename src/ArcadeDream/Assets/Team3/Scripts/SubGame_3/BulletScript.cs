using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;

    public float despawnTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(despawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ground" || collision.tag == "Breakable")
        Destroy(gameObject);
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
