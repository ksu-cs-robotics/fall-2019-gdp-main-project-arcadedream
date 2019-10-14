using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoalieMovement : MonoBehaviour
{
    public int leftlimit;
    public int rightlimit;
    // Start is called before the first frame update
    void Start()
    {
        int leftlimit = 0;
        int rightlimit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)&&leftlimit!=1)
        {
            this.gameObject.transform.Translate(-1, 0, 0);
            leftlimit++;
            rightlimit--;
        }

        if (Input.GetKeyDown(KeyCode.D)&&rightlimit!=1)
        {
            this.gameObject.transform.Translate(1, 0, 0);
            leftlimit--;
            rightlimit++;
        }
    }
}
