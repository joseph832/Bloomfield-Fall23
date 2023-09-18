using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class carController : MonoBehaviour
{
    [Header("Input Keys")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    [Header("Speed Vars")]
    public float speed = 2f;
    public GameObject myCar;
    public Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            myCar.transform.Translate(Vector3.up * speed);
            Debug.Log("W pressed");
        }
        if (Input.GetKey(downKey))
        {
            myCar.transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(leftKey))
        {
            myCar.transform.Rotate(new Vector3(0,0,speed*4f));
        }
        if (Input.GetKey(rightKey))
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
