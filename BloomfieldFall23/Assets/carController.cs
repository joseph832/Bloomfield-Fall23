using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    //speed is for forward/backwards, rotSpeed is for turning left/right

    [Header("movement vars")]
    public float speed = 2f;
    public float rotSpeed = 1f;
    public Vector3 myVec;

    [Header("KeyCodes")]
    public KeyCode forward = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    //int myScore is private - there's no public prefix
    //we do this so other scripts can't edit the player score accidentally
    private int myScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forward))
        {
            //manually adds a vector to the actual position part of the transform - the vector added this way is in world space
            //myCar.transform.position += Vector3.up * speed;
            //Debug.Log("W pressed");

            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(backwards))
        {
            transform.Translate(Vector3.right * speed);
        }
        if (Input.GetKey(left))
        {
            transform.Rotate(new Vector3(0,0,-1*rotSpeed));
        }
        if (Input.GetKey(right))
        {
            transform.Rotate(new Vector3(0, 0,1*rotSpeed));
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collided: " + collision.gameObject.name);
        //if(collision.gameObject.name == "Circle" || collision.gameObject.name == "Circle(Clone)")
        if(collision.gameObject.tag == "Collectibles")
        {
            //myScore = myScore + 1;
            myScore++;
            Destroy(collision.gameObject);
        }
    }

    //GET function to send the score out to another script if needed
    public int GetScore()
    {
        return myScore;
    }

    //SET function to change the score when it is time to
    public void SetScore(int score)
    {
        myScore = score;
    }

}
