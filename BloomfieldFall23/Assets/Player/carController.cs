using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class carController : MonoBehaviour
{
    //make our keybinds public so we can have player1 / player2 / etc
    [Header("Input Keys")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    [Header("Speed Vars")]
    public float speed = 2f; //modifies base speed by a multiplier
    public float rotationMod = 1f; //modifies rotation speed
    public GameObject myCar;
    public Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //our controller uses just if() statements so multiple inputs can be active at once
        if (Input.GetKey(upKey))
        {
            myCar.transform.Translate(Vector3.up * speed);
            //Debug.Log("W pressed"); 
        }
        if (Input.GetKey(downKey))
        {
            myCar.transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(leftKey))
        {
            myCar.transform.Rotate(new Vector3(0,0,1*rotationMod));
        }
        if (Input.GetKey(rightKey))
        {
            myCar.transform.Rotate(new Vector3(0, 0, -1*rotationMod));
        }

    }

    void FixedUpdate()
    {
        
    }
    //OnCollisionStay2D checks to see if ANY colliders are actively colliding
    //with other colliders in the scene, then gives us a Collision2D data to use
    void OnCollisionStay2D(Collision2D collision)
    {
        //we can check the Collision2D collision to see which game object it is by using
        //gameObject.name - this lets us set different behaviors on collision
        Debug.Log("collided with: " + collision.gameObject.name);
        if(collision.gameObject.name == "enemy(Clone)")
        {
            Destroy(collision.gameObject); 
            //if the player (this object) hits an enemy, destroy it
        }
    }
}
