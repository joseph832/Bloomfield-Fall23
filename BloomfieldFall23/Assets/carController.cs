using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float speed = 2f;
    public GameObject myCar;
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
}
