using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class gameManager : MonoBehaviour
{
    public float timer = 60f;
    float spawnTimer;
    public TextMeshProUGUI myTimerText;
    public GameObject myEnemy;

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
        spawnTimer += Time.deltaTime;
        timer -= Time.deltaTime;
        float timeDisplay = Mathf.RoundToInt(timer);
        Debug.Log("timer: " + timer + "timeDisplay: " + timeDisplay);
        myTimerText.text = timeDisplay.ToString();


        Vector3 targetPos = new Vector3(UnityEngine.Random.Range(myXbounds.x, myXbounds.y), UnityEngine.Random.Range(myYbounds.x, myYbounds.y), 0);
        if(spawnTimer > 2f)
        {
            Instantiate(myEnemy, targetPos, Quaternion.identity);
            spawnTimer = 0f;
        }
    }

}
