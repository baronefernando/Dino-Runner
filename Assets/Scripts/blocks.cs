using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blocks : MonoBehaviour
{
    public static Rigidbody2D obstacle;
    System.Random rnd = new System.Random();
    int rnum;
    public static byte speed;
    private Vector2 targetPos;
    public float Xincrement;
    public float airObstacle;
    public float groundObstacle;
    public float offsetRight;
    public float offsetLeft;
    Collider2D myObstacle;
    Collider2D myPowerUp;
    

    // Start is called before the first frame update
    void Start()
    {
       speed = 10;
       obstacle = GetComponent<Rigidbody2D>();
       myObstacle = GameObject.Find("obstacle").GetComponentInChildren<Collider2D>();
       myPowerUp = GameObject.Find("powerup").GetComponentInChildren<Collider2D>();
       Physics2D.IgnoreCollision(myPowerUp, myObstacle);
    }

    // Update is called once per frame
    void Update()
    {
        StartBlocks();
    }

    void StartBlocks()
    {
        if (obstacle.position.x <= offsetLeft)
        {
            rnum = rnd.Next(1, 3); //Generates a new random number
            if (rnum == 1)
            {
                obstacle.position = new Vector2(offsetRight, airObstacle);
                scoreScript.scored = false;
            }
            else if (rnum == 2)
            {
                obstacle.position = new Vector2(offsetRight, groundObstacle);
                scoreScript.scored = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (scoreScript.scored == false && power_ups.extraLife == true)
        {
            power_ups.lifeCount -= 1;
            if(power_ups.lifeCount == 0)
            {
                power_ups.extraLife = false;
            }
        }
        else if (scoreScript.scored == false && power_ups.extraLife == false)
        {
            speed = 0;
            obstacle.position = new Vector2(offsetRight, groundObstacle);
            SceneManager.LoadScene("GameOver");
        }
    }
}
