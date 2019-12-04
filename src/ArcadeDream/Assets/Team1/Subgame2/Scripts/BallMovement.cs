using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Pun;

public class BallMovement : MonoBehaviourPunCallbacks
{
    float speed = .05f;
    float radius;
    Vector2 direction;
    Rigidbody2D _rigidbody;
    public Color color = Color.white;

    public float maxVelocity;

    private bool stop = true;

    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        //change direction to random direction
        direction = Vector2.one.normalized; //direction is (1,1) normalized
        radius = transform.localScale.x / 2; //half of width
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ResetBall());

        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

     if(PhotonNetwork.IsMasterClient)
        {
            if (_rigidbody.velocity == Vector2.zero && !stop)
            {
                speed = .05f;
                Vector2 randomDir = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
                _rigidbody.AddForce(randomDir * speed);
            }

            if (stop) this.GetComponent<Transform>().position = Vector3.zero;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxVelocity);
        }
       
    }

    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GetComponent<Rigidbody2D>().position);
            stream.SendNext(GetComponent<Rigidbody2D>().rotation);
            stream.SendNext(GetComponent<Rigidbody2D>().velocity);
        }
        else
        {
            GetComponent<Rigidbody2D>().position = (Vector3)stream.ReceiveNext();
            GetComponent<Rigidbody2D>().rotation = (float)stream.ReceiveNext();
            GetComponent<Rigidbody2D>().velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            GetComponent<Rigidbody2D>().position += GetComponent<Rigidbody2D>().velocity * lag;
        }
    }

    */


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "paddle1" || collision.tag == "paddle2" || collision.tag == "paddle3" || collision.tag == "paddle4")
        {
            Debug.Log("Player Bounce");
            //Changes the color of the ball based on the last player to hit it last
            switch (collision.tag)
            {
                case "paddle1":
                    color = Color.red;
                    break;
                case "paddle2":
                    color = Color.blue;
                    break;
                case "paddle3":
                    color = Color.green;
                    break;
                case "paddle4":
                    color = Color.yellow;
                    break;
                default:
                    break;
            }
            collision.GetComponent<AudioSource>().Play();
            //speed += 0.01f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            gameObject.GetComponent<TrailRenderer>().startColor = color;
            gameObject.GetComponent<TrailRenderer>().endColor = color;

            //Experimentation
            Vector2 newDirection = collision.GetComponent<PlayerPaddleMovement>().ballDirection;

            if (newDirection != Vector2.zero && !stop)
            {

                direction = newDirection;
                speed = collision.GetComponent<Rigidbody2D>().velocity.magnitude;
            }
            _rigidbody.AddForce(direction * speed);
        }

        if (collision.tag == "GoalZone")
        {
            Debug.Log("Goal!");
            collision.GetComponent<GoalLivesManager>().GainPoint(GetComponent<SpriteRenderer>().color);
            collision.GetComponent<AudioSource>().Play();
            //reset ball
            StartCoroutine(ResetBall());
            //ResetDirection();
            //player loses life
            //players lose all powerups
        }
    }

    IEnumerator ResetBall()
    {

        this.GetComponent<Transform>().position = Vector3.zero; // move ball to middle
        stop = true;
        _rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        Debug.Log("Goal reset");
        stop = false;

    }

    void ResetDirection()
    {
        ResetBall();
        //Vector2 randomDir = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
        //_rigidbody.AddForce(randomDir * speed);
        _rigidbody.velocity = Vector3.zero;
    }

}
