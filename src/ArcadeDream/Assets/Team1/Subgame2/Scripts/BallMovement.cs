using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BallMovement : NetworkBehaviour
{
    float speed = .05f;
    float radius;
    Vector2 direction;
    Rigidbody2D _rigidbody;
    [SyncVar] public Color color = Color.white;

    public float maxVelocity;


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
            Vector2 randomDir = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
            _rigidbody.AddForce(randomDir * speed);
        }
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxVelocity);

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player Bounce");
            //Changes the color of the ball based on the last player to hit it last
            switch (collision.name)
            {
                case "Player1Paddle":
                    color = Color.red;
                    break;
                case "Player2Paddle":
                    color = Color.blue;
                    break;
                case "Player3Paddle":
                    color = Color.green;
                    break;
                case "Player4Paddle":
                    color = Color.yellow;
                    break;
                default:
                    break;
            }
            collision.GetComponent<AudioSource>().Play();
            speed += 0.01f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            gameObject.GetComponent<TrailRenderer>().startColor = color;
            gameObject.GetComponent<TrailRenderer>().endColor = color;


        }

        if (collision.tag == "GoalZone")
        {
            Debug.Log("Goal!");
            collision.GetComponent<GoalLivesManager>().GainPoint(GetComponent<SpriteRenderer>().color);
            collision.GetComponent<AudioSource>().Play();
            //reset ball
            ResetBall();
            //ResetDirection();
            //player loses life
            //players lose all powerups
        }
    }

    void ResetBall()
    {
        this.GetComponent<Transform>().position = Vector3.zero; // move ball to middle
        _rigidbody.velocity = Vector3.zero;
        Debug.Log("Goal reset");

    }

    void ResetDirection()
    {
        ResetBall();
        Vector2 randomDir = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
        _rigidbody.AddForce(randomDir * speed);
        _rigidbody.velocity = Vector3.zero;
    }

}
