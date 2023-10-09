using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class physicsGameManager : MonoBehaviour
{

    public float timer = 60f;
    public float spawnInterval = 2f;
    float spawnTimer;

    //public TextMeshProUGUI myTimerText;
    //public TextMeshProUGUI playerScore;

    public GameObject myPlayer;
    playerController3D myPlayerController;

    public GameObject myEnemy;

    //we can use Vector2s to represent the X and Y boundaries of our game scene
    public Vector2 myXbounds;
    public Vector2 myYbounds;

    // Start is called before the first frame update
    void Start()
    {

        timer = 60f;
        spawnTimer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //spawnTimer counts up for enemy spawn while timer counts down for game time
        spawnTimer += Time.deltaTime;
       // timer -= Time.deltaTime;

        //Mathf. includes a lot of useful math functions we can use
        //here we're using RoundToInt() to make a cleaner in-game clock display (no decimals)
       // float timeDisplay = Mathf.RoundToInt(timer);
       // Debug.Log("timer: " + timer + "timeDisplay: " + timeDisplay);
       // myTimerText.text = timeDisplay.ToString();

        //this line sets the enemy spawn to a random position inside the game bounds
        Vector3 targetPos = new Vector3(UnityEngine.Random.Range(myXbounds.x, myXbounds.y), 2f, UnityEngine.Random.Range(myYbounds.x, myYbounds.y));
        
        //every 2 seconds, spawn an enemy
        if(spawnTimer > spawnInterval)
        {
            //instantiate creates a new game object at the given pos/rot 
            //this can be a prefab from inside your assets folder

            //instantiate a new enemy - be sure to declare it so we can assign it a player
            spawnRamp(myEnemy, targetPos);

            spawnTimer = 0f; //reset spawn timer on spawn
        }


    }

    void spawnCube(GameObject myCube, Vector3 targetPos)
    {
        //instantiate a new enemy - be sure to declare it so we can assign it a player
        GameObject newEnemy = Instantiate(myCube, targetPos, Quaternion.identity);
        cubeEnemy newScript = newEnemy.GetComponent<cubeEnemy>();
        newScript.SetPlayer(myPlayer);
    }

    void spawnRamp(GameObject myRamp, Vector3 targetPos)
    {
        //same deal but for the ram
        GameObject newEnemy = Instantiate(myRamp, targetPos, Quaternion.identity);
        rampEnemy newScript = newEnemy.GetComponent<rampEnemy>();
        newScript.SetPlayer(myPlayer);

    }

}
