using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class physicsGameManager : MonoBehaviour
{

    public float timer = 60f;
    public float waveInterval = 20f;
    float waveTimer;
    float waveCount;

    //public TextMeshProUGUI myTimerText;
    //public TextMeshProUGUI playerScore;

    public GameObject myPlayer;
    playerController3D myPlayerController;

    public GameObject myEnemy;

    //we can use Vector2s to represent the X and Y boundaries of our game scene
    public Vector2 myXbounds;
    public Vector2 myYbounds;


    public GameObject[] wave1 = null;
    public GameObject[] wave2 = null;
    public GameObject[] wave3 = null;
    

    // Start is called before the first frame update
    void Start()
    {
        waveCount = 0;
        timer = 60f;
        waveTimer = waveInterval-2f;

    }

    // Update is called once per frame
    void Update()
    {
        //waveTimer counts up for enemy spawn while timer counts down for game time
        waveTimer += Time.deltaTime;
       // timer -= Time.deltaTime;

        //Mathf. includes a lot of useful math functions we can use
        //here we're using RoundToInt() to make a cleaner in-game clock display (no decimals)
       // float timeDisplay = Mathf.RoundToInt(timer);
       // Debug.Log("timer: " + timer + "timeDisplay: " + timeDisplay);
       // myTimerText.text = timeDisplay.ToString();

  
        
        //every 30 seconds, spawn a wave of enemies
        if(waveTimer > waveInterval)
        {
            waveCount++; //add to the wave tracker, so we don't double spawn 1 wave
            int waveLength = 0;
            
            if(waveCount == 1) 
            {
                waveLength = wave1.Length; //get length of the wave we need
                SpawnWave(wave1, waveLength);
            }

            else if(waveCount == 2) 
            { 
                waveLength = wave2.Length;
                SpawnWave(wave2, waveLength);
            }

            else if(waveCount == 3) 
            { 
                waveLength = wave3.Length;
                SpawnWave(wave3, waveLength);
            }

            else { waveLength = 0; }

            waveTimer = 0f; //reset spawn timer on spawn
            waveInterval = waveInterval *= 1.2f;
        }


    }

    void SpawnCube(GameObject myCube, Vector3 targetPos)
    {
        //instantiate a new enemy - be sure to declare it so we can assign it a player
        //be sure to declare the GameObject so we can find its components and edit the target player
        GameObject newEnemy = Instantiate(myCube, targetPos, Quaternion.identity);
        //set the script and reference the player variable in our gameManager
        cubeEnemy newScript = newEnemy.GetComponent<cubeEnemy>();
        newScript.SetPlayer(myPlayer);
    }

    void SpawnRamp(GameObject newEnemy,  Vector3 targetPos)
    {
        //this time we just call the different enemy script
        rampEnemy newScript = newEnemy.GetComponent<rampEnemy>();
        newScript.SetPlayer(myPlayer);

    }

    void SpawnWave(GameObject[] myWave, int waveCount)
    {
        for (int i = 0; i < waveCount; i++) //spawn an enemy of each type for each index in the array
        {
            //this line sets the enemy spawn to a random position inside the game bounds
            Vector3 targetPos = new Vector3(UnityEngine.Random.Range(myXbounds.x, myXbounds.y), 
                                            2f, 
                                            UnityEngine.Random.Range(myYbounds.x, myYbounds.y));
            
            //spawn an enemy of the type specified in array
            GameObject newEnemy = Instantiate(myWave[i], targetPos, Quaternion.identity);

            //check tags, set up enemy if valid, otherwise return debug log INVALID
            if (newEnemy.tag == "ramp") { SpawnRamp(newEnemy, targetPos); }
            else if (newEnemy.tag == "cube") { SpawnCube(newEnemy, targetPos); }
            else { Debug.Log("INVALID enemy type"); }
        }
    }
}
