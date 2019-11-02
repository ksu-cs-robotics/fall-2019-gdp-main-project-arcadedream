using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerAI : MonoBehaviour
{

    GameObject AIpaddle;
    GameObject AIgoalie;
    GameObject target;
    static private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Single Player script active");
        AIgoalie = GameObject.Find("Player2Goalie");
        AIpaddle = GameObject.Find("Player2Paddle");
        target = GameObject.Find("Ball(Clone)");
        coroutine = AIMovements();
        StartCoroutine(coroutine);
        StartCoroutine("MovePaddle");
    }

    //AI movements are performed every few seconds based on the whether its goalie or paddle
    IEnumerator AIMovements()
    {
        while (true)
        {
            yield return StartCoroutine("MoveGoalie");
        }
    }

    IEnumerator MoveGoalie()
    {
        Debug.Log(AIgoalie.transform.position.x + " " + AIgoalie.transform.position.y);
        Vector3 point1, point2, point3; //points of the goalie
        point1 = new Vector3(-4.5f, 3.5f, 0); //left position
        point2 = new Vector3(-3.5f, 3.5f, 0); //middle position
        point3 = new Vector3(-3.5f, 4.5f, 0); //right position
        float distance = Vector3.Distance(point1, AIgoalie.transform.position);
        float distance2 = Vector3.Distance(point2, AIgoalie.transform.position);
        float distance3 = Vector3.Distance(point3, AIgoalie.transform.position);

        if (AIgoalie.transform.position == point1)
        {
            if (distance2 <= distance3)
            {
                AIgoalie.transform.position = point2;
            }
            else
            {
                AIgoalie.transform.position = point3;
            }
        }
        if (AIgoalie.transform.position == point2)
        {
            if (distance <= distance3)
            {
                AIgoalie.transform.position = point1;
            }
            else
            {
                AIgoalie.transform.position = point3;
            }
        }
        if (AIgoalie.transform.position == point3)
        {
            if (distance <= distance2)
            {
                AIgoalie.transform.position = point1;
            }
            else
            {
                AIgoalie.transform.position = point2;
            }
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MovePaddle()
    {
        while (true)
        {
            float dis = Vector3.Distance(AIpaddle.transform.position, target.transform.position);
            Debug.Log(dis);
            if (dis > 1.5f)
            {
                AIpaddle.transform.position = Vector3.MoveTowards(AIpaddle.transform.position, target.transform.position, 0.05f);
            }
                yield return new WaitForSeconds(0.001f);
        }
    }
}
