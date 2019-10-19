using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    float speed = .05f;
    float radius;
    Vector2 direction;
    Rigidbody2D _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        //change direction to random direction
        direction = Vector2.one.normalized; //direction is (1,1) normalized
        radius = transform.localScale.x / 2; //half of width
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity == Vector2.zero)
        {
            Vector2 randomDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            _rigidbody.AddForce(randomDir * speed);
        }
        /*
        //Ball movement
        //transform.Translate(direction * speed * Time.deltaTime);
        //Bounce off bottom
        if(transform.position.y < PongGameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        //Bound off Top
        if(transform.position.y > PongGameManager.topRight.y - radius && direction.y > 0){
            direction.y = -direction.y;
        }
        //Bound off Right
        if (transform.position.x > PongGameManager.topRight.x - radius && direction.x > 0)
        {
            direction.x = -direction.x;
        }
        //Bound off Left
        if (transform.position.x < PongGameManager.topLeft.x + radius && direction.x < 0)
        {
            direction.x = -direction.x;
        }*/
        //Game over
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player Bounce");
            //Changes the color of the ball based on the last player to hit it last
            GetComponent<SpriteRenderer>().color = collision.GetComponent<SpriteRenderer>().color;
            speed += 0.01f;
        }

        if (collision.tag == "GoalZone")
        {
            Debug.Log("Goal!");
            collision.GetComponent<GoalLivesManger>().LoseLife();
            //reset game
            //player loses life
            //players lose all powerups
        }
    }
}
