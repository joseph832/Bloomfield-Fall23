using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rampEnemy : MonoBehaviour
{
    public float speed;
    public GameObject targetPlayer;
    Rigidbody myRB;
    public float slideFriction = 1f;

    //ramps will target the player then stop once close enough and just launch faster
    //we'll use a distance to player threshold and an accelerator to get this behavior
    public float trackThreshold = 1f;
    public float accel = 2f;
    public bool stopTracking; //bool to disable tracking in movement code

    // Start is called before the first frame update
    void Start()
    {
        //we find the rigidbody - running this in start because it's more efficient
        //GetComponent is a search/find function so it's inefficient to run in update/ will slow your game down
        myRB = GetComponent<Rigidbody>();
        stopTracking = false;
    }

    void FixedUpdate()
    {
        //find our player to bother
        Vector3 playerPos = targetPlayer.transform.position;

        //finding direction towards the player by subtracting our position from theirs
        Vector3 dirTowards = (playerPos - transform.position);
        //double checking our math just in case with a debug
        Debug.DrawRay(transform.position, dirTowards, Color.black, .1f);

        //remove the y axis to keep the enemies grounded
        dirTowards = new Vector3(dirTowards.x, 0f, dirTowards.z);

        //check distance to player, turn*2off tracking once the ramp gets
        //close enough to the player
        if(dirTowards.magnitude < trackThreshold) { stopTracking = true; }

        //add force towards the player if the ramp is still far away
        if (!stopTracking) { myRB.AddForce(dirTowards.normalized * speed); }
        //once tracking is turned off (ramp is close to player) add a higher force in existing dir
        else { myRB.AddForce(myRB.velocity.normalized * speed * accel); }


        if(myRB.velocity.magnitude > 0f) 
        {
            Vector3 stopVel = Vector3.ClampMagnitude(-myRB.velocity.normalized * slideFriction, myRB.velocity.magnitude);
            myRB.AddForce(stopVel); 
        }
    }

    public void SetPlayer(GameObject player) //this is here for the gameManager to call on enemy spawn
    {
        targetPlayer = player;
    }
}
