using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class playerController3D : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 sensMouse = new Vector2 (.5f,.5f);
    public GameObject myCam;
    public Vector2 camVertLock = new Vector2(90,-90);
    Vector3 myDir;

    float rotY;
    float rotX;
    // Start is called before the first frame update
    void Start()
    {
        myDir = Vector3.zero;
        rotY = 0f;
        rotX = 0f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        //debugs for player: look angles
        //first we take a vector from the camera transform then use TransformDirection to convert it from
        //local to world space. Then we draw a ray using that vector to display player look (forward) and left/right 
        Vector3 playerLook = myCam.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(myCam.transform.position, playerLook*2f, Color.green, .3f);

        //for the left/ right vectors we do the same but with the corresponding vector.left or vector.right shorthand
        //then we only pull two of the 3 values since we want to render on a flat 2D plane, not full 3D space
        Vector3 playerLeft = myCam.transform.TransformDirection(Vector3.left);
        playerLeft = new Vector3(playerLeft.x, 0f, playerLeft.z);
        Debug.DrawRay(transform.position, playerLeft, Color.green, .3f);

        //right works the same as the left drawRay, more or less
        Vector3 playerRight = myCam.transform.TransformDirection(Vector3.right);
        playerRight = new Vector3(playerRight.x, 0f, playerRight.z);
        Debug.DrawRay(transform.position, playerRight, Color.green, .3f);

        //float x & z control the translate/movement inputs, taken from Unity preferences
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //whenever working with vectors for movement or rotation it's good practice
        //to .normalized them - this sets their magnitude equal to 1 and makes them cleaner to work with
        Vector3 inputDir = transform.rotation * new Vector3(x, 0f, z).normalized;

        //after normalizing we multiply the vector by our speed variable to set our player speed in a
        //clean and consistent way. We also set the Translate in Space.World, since our input axes give us world coordinates
        transform.Translate(inputDir*speed, Space.World);
        Debug.DrawRay(this.transform.position, inputDir * 5f, Color.yellow, 20f);


        //rotations Y&X are pulled from mouse look, go into camera controls
        rotY += Input.GetAxisRaw("Mouse X") * sensMouse.y;
        rotX += Input.GetAxisRaw("Mouse Y") * sensMouse.x;

        //clamp our Y to avoid looking to far up or down
        if(rotX > camVertLock.x)
        {
            rotX = camVertLock.x;
        }
        if(rotX < camVertLock.y)
        {
            rotX = camVertLock.y;
        }

        //apply the rotations to the player and camera
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
        myCam.transform.rotation = Quaternion.Euler(-rotX, rotY, 0f);


    }
}
