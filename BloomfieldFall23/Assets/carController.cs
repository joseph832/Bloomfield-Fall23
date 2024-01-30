using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float speed = 2f;
    public GameObject myCar;
    public Input input;
    public Vector3 myVec;
    public int myScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myCar.transform.position += Vector3.up * speed;
            Debug.Log("W pressed");
        }
        if (Input.GetKey(KeyCode.S))
        {
            myCar.transform.position += Vector3.down * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myCar.transform.position += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myCar.transform.position += Vector3.right * speed;
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

    public int GetScore()
    {
        return myScore;
    }

}
