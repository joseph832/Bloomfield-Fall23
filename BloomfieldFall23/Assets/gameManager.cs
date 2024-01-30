using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("Timer Vars")]
    public float timer;
    public TextMeshProUGUI currentTime;
    public float enemyTimer;
    public float spawnInterval = 2f;
    public float timeLimit = 60f;

    [Header("Player Vars")]
    public GameObject myPlayer;
    public float myScore;
    public TextMeshProUGUI scoreText;
    carController myController;

    [Header("Collectibles")]
    public GameObject myEnemy;

    //declare bounding box for my scene
    public Vector2 myXbounds;
    public Vector2 myYbounds;



    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        myController = myPlayer.GetComponent<carController>();
    }

    // Update is called once per frame
    void Update()
    {
        myScore = myController.GetScore();
        scoreText.text = myScore.ToString();

        //deltaTime is the amount of time that has passed from one Update() to the next
        timer += Time.deltaTime;

        //Mathf is a class in unity that has a variety of math functions built in
        float timeLeft = Mathf.Round(60f - timer);
        //set the UI timer text to time left
        currentTime.text = timeLeft.ToString();

        //our enemyTimer will be used to spawn red circle prefabs
        enemyTimer += Time.deltaTime;

        //this turns off the player when the time limit is reached
        if(timer > timeLimit)
        {
            myPlayer.SetActive(false);
        }

        //this spawns an enemy on a given interval, then resets the timer
        Vector3 targetPos = new Vector3(Random.Range(myXbounds.x,myXbounds.y), Random.Range(myYbounds.x, myYbounds.y),0);
        if(enemyTimer > spawnInterval)
        {
            enemyTimer = 0f;
            Instantiate(myEnemy, targetPos, Quaternion.identity);
        }
    }

    public float GetTime()
    {
        return timer;
    }
}
