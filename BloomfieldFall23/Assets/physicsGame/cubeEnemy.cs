using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeEnemy : MonoBehaviour
{
    public float speed;
    public GameObject targetPlayer;
    Rigidbody myRB;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //find our player to bother
        Vector3 playerPos = targetPlayer.transform.position;

        //finding direction towards the player by subtracting our position from theirs
        Vector3 dirTowards = (playerPos - transform.position).normalized;
        //double checking our math just in case with a debug
        Debug.DrawRay(transform.position, dirTowards, Color.black, .1f);

        //add force towards the player
        myRB.AddForce(dirTowards * speed);
    }

    public void SetPlayer(GameObject player)
    {
        targetPlayer = player;
    }
}
