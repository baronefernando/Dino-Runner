using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public static int scoreValue;
    Text score;
    Text highScore;
    public static bool scored;
    public static int highscoreValue;
    int scoreToSpeed;
    Collider2D scoreRecorder;
    Collider2D myPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        scoreRecorder = GameObject.Find("scoredetector").GetComponentInChildren<Collider2D>();
        myPowerUp = GameObject.Find("powerup").GetComponentInChildren<Collider2D>();
        Physics2D.IgnoreCollision(myPowerUp, scoreRecorder);

        score = GameObject.Find("myScore").GetComponentInChildren<Text>();
        highScore = GameObject.Find("myHighScore").GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "SCORE: " + scoreValue;
        if(PlayerPrefs.HasKey("hscore"))
        {
            highscoreValue = PlayerPrefs.GetInt("hscore");
            highScore.text = "BEST: " + highscoreValue;
        }
        else
        {
            highScore.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scoreValue += 10;
        scored = true;
        scoreToSpeed += 10;
        increaseSpeed();
        if (scoreValue > highscoreValue)
        {
            PlayerPrefs.SetInt("hscore", scoreValue);
            highscoreValue = PlayerPrefs.GetInt("hscore");
        }
    }
    private void increaseSpeed()
    {
        if(scoreToSpeed == 100)
        {
            scoreToSpeed = 0;
            if(blocks.speed < 22)
            {
                blocks.speed += 1;
                power_ups.speed += 1;
            }
            
            if (blocks.speed >= 18 && blocks.speed < 22)
            {
                jump.rb.gravityScale += 1;
                jump.jumpForce += 1;
            } else if (blocks.speed < 18)
            {
                jump.rb.gravityScale = 5;
                jump.jumpForce = 15;
            }
        }
    }
}
