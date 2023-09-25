using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class playerController3D : MonoBehaviour
{
    public float speed = 1f;
    public GameObject myCam;
    public GameObject cube1;
    public GameObject cube2;
    Vector3 myDir;

    float rotY;
    float rotX;
    // Start is called before the first frame update
    void Start()
    {
        myDir = Vector3.zero;
        rotY = 0f;
        rotX = 0f;
        Debug.DrawLine(cube1.transform.position, cube2.transform.position, Color.white, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        //float x & z control the translate/movement inputs, taken from Unity preferences
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = transform.rotation * new Vector3(x, 0f, z).normalized;

        Debug.DrawRay(this.transform.position, inputDir * 5f, Color.yellow, 20f);

        transform.Translate(inputDir);

        //rotations Y&X are pulled from mouse look, go into camera controls
        rotY += Input.GetAxisRaw("Mouse X");
        rotX += Input.GetAxisRaw("Mouse Y");

        //clamp our Y to avoid looking to far up or down
        if(rotX > 90f)
        {
            rotX = 90f;
        }
        if(rotX < -90f)
        {
            rotX = -90f;
        }

        //apply the rotations to the player and camera
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
        myCam.transform.rotation = Quaternion.Euler(rotX, rotY, 0f);

    }
}
