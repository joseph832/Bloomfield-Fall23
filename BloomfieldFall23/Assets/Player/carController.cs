using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class carController : MonoBehaviour
{
    public int myInteger;
    public double myDouble;
    public float speed = 2f;

    public GameObject myCar;
    public Transform myTransform;

    public string myString = "hello World";
    public bool trueFalse = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myCar.transform.Translate(Vector3.up * speed);
            Debug.Log("W pressed");
        }
        if (Input.GetKey(KeyCode.S))
        {
            myCar.transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myCar.transform.Rotate(new Vector3(0,0,speed*4f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            myCar.transform.Rotate(new Vector3(0, 0, -speed * 4f));
        }

    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collided with: " + collision.gameObject.name);
        if(collision.gameObject.name == "enemy(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }
}
