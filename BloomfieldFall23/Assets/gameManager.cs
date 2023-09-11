using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public float timer;
    public float fixedTimer;
    public TextMeshProUGUI myTimerText;
    public GameObject myEnemy;

    public Vector2 myXbounds;
    public Vector2 myYbounds;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        myTimerText.text = timer.ToString();


        Vector3 targetPos = new Vector3(Random.Range(myXbounds.x, myXbounds.y), Random.Range(myYbounds.x, myYbounds.y), 0);
        if(timer > 2f)
        {
            Instantiate(myEnemy, targetPos, Quaternion.identity);
            timer = 0f;
        }
    }

}
