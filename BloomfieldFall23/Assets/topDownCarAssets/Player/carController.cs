using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public gameManager myMgr; //link the gameManager so we can call func, use get/set 
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

    [Header("Audio Vars")]
    public AudioClip[] myCrashes; //in editor assign as many audio clips as you want
    AudioSource crashSource; //the audio source attached to the player prefab

    private int myScore;

    // Start is called before the first frame update
    void Start()
    {
        myScore = 0;
        crashSource = GetComponent<AudioSource>(); //find the audio source component
        if(this.gameObject.name != "Player") { crashSource.enabled = false; } //check to make sure player2 does not play audio
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
            myMgr.PlaySquish(); //sound sources are all saved on the gameManager class
            Destroy(collision.gameObject);
            myScore += 1;
            //if the player (this object) hits an enemy, destroy it
        }
        else if(collision.gameObject.tag == "Player" && this.gameObject.name == "Player")
        {
            PlayCrash();
        }
    }

    public int GetScore()
    {
        return myScore;
    }

    public void PlayCrash()
    {
        int i = UnityEngine.Random.Range(0, myCrashes.Length - 1);
        crashSource.clip = myCrashes[i];
        crashSource.Play();
    }

}
