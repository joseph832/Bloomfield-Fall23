using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class gameManager : MonoBehaviour
{

    public float timer = 60f;
    public float spawnInterval = 2f;
    float spawnTimer;

    public TextMeshProUGUI myTimerText;
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
        timer -= Time.deltaTime;

        //Mathf. includes a lot of useful math functions we can use
        //here we're using RoundToInt() to make a cleaner in-game clock display (no decimals)
        float timeDisplay = Mathf.RoundToInt(timer);
        Debug.Log("timer: " + timer + "timeDisplay: " + timeDisplay);
        myTimerText.text = timeDisplay.ToString();

        //this line sets the enemy spawn to a random position inside the game bounds
        Vector3 targetPos = new Vector3(UnityEngine.Random.Range(myXbounds.x, myXbounds.y), UnityEngine.Random.Range(myYbounds.x, myYbounds.y), 0);
        
        //every 2 seconds, spawn an enemy
        if(spawnTimer > spawnInterval)
        {
            //instantiate creates a new game object at the given pos/rot 
            //this can be a prefab from inside your assets folder
            Instantiate(myEnemy, targetPos, Quaternion.identity);
            spawnTimer = 0f; //reset spawn timer on spawn
        }
    }

}
