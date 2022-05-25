﻿﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class power_ups : MonoBehaviour
{
    Rigidbody2D powerup;
    Rigidbody2D obstacle;
    Collider2D playerCollider;
    Collider2D myObstacle;
    Collider2D myPowerUp;
    public static byte speed;
    private Vector2 targetPos;
    public float Xincrement;
    public float airObstacle;
    public float offsetRight;
    public float offsetLeft;
    public float ObstacleOffsetRight;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public static byte lifeCount = 0;

    int number;
    int rnum;
    int previousNumber;
    public static int rnum_type;
    int rnum_draw;
    public static int RATIO_CHANCE_A = 75;
    public static int RATIO_CHANCE_B = 25;
    public static int RATIO_TOTAL = RATIO_CHANCE_A + RATIO_CHANCE_B;

    bool allowSpawn;
    bool spriteChanged;
    public static bool extraLife = false;

    Text timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        powerup = GetComponent<Rigidbody2D>();
        playerCollider = GameObject.Find("player").GetComponentInChildren<Collider2D>();
        myObstacle = GameObject.Find("obstacle").GetComponentInChildren<Collider2D>();
        myPowerUp = GameObject.Find("powerup").GetComponentInChildren<Collider2D>();
        obstacle = GameObject.Find("obstacle").GetComponentInChildren<Rigidbody2D>();
        timeLeft = GameObject.Find("timeLeft").GetComponentInChildren<Text>();
        timeLeft.enabled = false;
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        StartBlocks();
        lifeChecker();
    }
    void ChangeSprite(int sprite)
    {
        spriteRenderer.sprite = spriteArray[sprite];
    }
    void CollisionDetector(bool collide)
    {
        if (collide == true)
        {
            Physics2D.IgnoreCollision(myObstacle, playerCollider);
            Physics2D.IgnoreCollision(myPowerUp, playerCollider);
        }
        else if (collide == false)
        {
            Physics2D.IgnoreCollision(myObstacle, playerCollider, false);
            Physics2D.IgnoreCollision(myPowerUp, playerCollider, false);
        }
    }
    void StartBlocks()
    {
        if(powerup.position.x <= offsetLeft)
        {
            rnum = 0;
            allowSpawn = false;
            powerup.position = new Vector2(offsetRight, airObstacle);
            
        }
        if(obstacle.position.x == ObstacleOffsetRight)
        {
            allowSpawn = true;
        }
        
        if (scoreScript.scoreValue > 200 && allowSpawn && rnum == 2)
        {
            if (rnum_type == 1 && spriteChanged == false)
            {
                ChangeSprite(0);
                spriteChanged = true;
            }
            else if (rnum_type == 2 && spriteChanged == false)
            {
                ChangeSprite(1);
                spriteChanged = true;
            }
            else if (rnum_type == 3 && spriteChanged == false)
            {
                ChangeSprite(2);
                spriteChanged = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (obstacle.position.x <= ObstacleOffsetRight)
        {
            allowSpawn = false;
        }
        if (rnum_type == 1 && power_ups_selector.powerup_Enabled[0] == true) //Slow
        {
            rnum = 1;
            powerup.position = new Vector2(offsetRight, airObstacle);
            if (scoreScript.scoreValue > 500 && blocks.speed >= 18)
            {
                blocks.speed -= 6;
                speed -= 6;
            }
            else if (blocks.speed < 18 && blocks.speed > 11)
            {
                blocks.speed -= 2;
                speed -= 2;
            }
        }
        else if (rnum_type == 2 && power_ups_selector.powerup_Enabled[1] == true) //Mega boost
        {
            rnum = 1;
            powerup.position = new Vector2(offsetRight, airObstacle);
            blocks.speed += 30;
            speed += 30;
            StartCoroutine(startInvencibility());
        }
        else if (rnum_type == 3 && power_ups_selector.powerup_Enabled[2] == true) //Extra life
        {
            rnum = 1;
            powerup.position = new Vector2(offsetRight, airObstacle);
            if (lifeCount < 3)
            {
                lifeCount += 1;
            }
            extraLife = true;
        }
    }
    IEnumerator SpawnPowerUp()
    {
        spriteChanged = false;
        if(power_ups_selector.powerup_Enabled[0] == false && power_ups_selector.powerup_Enabled[1] == false && power_ups_selector.powerup_Enabled[2] == false)
        {
            yield break;
        }
        else if (power_ups_selector.powerup_Enabled[0] == false && power_ups_selector.powerup_Enabled[1] == true && power_ups_selector.powerup_Enabled[2] == true)
        {
            rnum_draw = Random.Range(1, 3);
            if(rnum_draw == 1)
            {
                rnum_type = 2;
            }
            else if (rnum_draw == 2)
            {
                rnum_type = 3;
            }

        }
        else if (power_ups_selector.powerup_Enabled[0] == true && power_ups_selector.powerup_Enabled[1] == false && power_ups_selector.powerup_Enabled[2] == true)
        {
            rnum_draw = Random.Range(1, 3);
            if (rnum_draw == 1)
            {
                rnum_type = 1;
            }
            else if (rnum_draw == 2)
            {
                rnum_type = 3;
            }
        }
        else if (power_ups_selector.powerup_Enabled[0] == true && power_ups_selector.powerup_Enabled[1] == true && power_ups_selector.powerup_Enabled[2] == false)
        {
            rnum_draw = Random.Range(1, 3);
            if (rnum_draw == 1)
            {
                rnum_type = 1;
            }
            else if (rnum_draw == 2)
            {
                rnum_type = 2;
            }
        }
        else if (power_ups_selector.powerup_Enabled[0] == true && power_ups_selector.powerup_Enabled[1] == true && power_ups_selector.powerup_Enabled[2] == true)
        {
            rnum_draw = Random.Range(1, 4);
            rnum_type = rnum_draw;
        }
        else if (power_ups_selector.powerup_Enabled[0] == true && power_ups_selector.powerup_Enabled[1] == false && power_ups_selector.powerup_Enabled[2] == false)
        {
            rnum_type = 1;
        }
        else if (power_ups_selector.powerup_Enabled[0] == false && power_ups_selector.powerup_Enabled[1] == true && power_ups_selector.powerup_Enabled[2] == false)
        {
            rnum_type = 2;
        }
        else if (power_ups_selector.powerup_Enabled[0] == false && power_ups_selector.powerup_Enabled[1] == false && power_ups_selector.powerup_Enabled[2] == true)
        {
            rnum_type = 3;
        }

        yield return new WaitForSeconds(8);
        number = Random.Range(0, RATIO_TOTAL);
        if (number < RATIO_CHANCE_A) //75% Chance
        {
            rnum = 2;
        }
        else if (number < RATIO_CHANCE_A + RATIO_CHANCE_B) //25% Chance
        {
            rnum = 0;
        }
        StartCoroutine(SpawnPowerUp());
    }
    IEnumerator startInvencibility()
    {
        CollisionDetector(true);
        StartCoroutine(powerupCounter());
        yield return new WaitForSeconds(5);
        blocks.speed -= 30;
        speed -= 30;
        timeLeft.text = "GET READY!!";
        yield return new WaitForSeconds(1);
        CollisionDetector(false);
        timeLeft.enabled = false;
    }
    IEnumerator powerupCounter()
    {
        Renderer myPlayer;
        myPlayer = GameObject.Find("player").GetComponentInChildren<Renderer>();
        
        timeLeft.enabled = true;
        for (int i = 5; i > 0; i--)
        {
            timeLeft.text = i.ToString();
            myPlayer.material.SetColor("_Color", Color.gray);
            yield return new WaitForSeconds(1);
            myPlayer.material.SetColor("_Color", Color.white);
        }
    }
   private void lifeChecker()
    {
        Renderer[] myLife = new Renderer[3];
        myLife[0] = GameObject.Find("holderUI").GetComponentInChildren<Renderer>();
        myLife[1] = GameObject.Find("holderUI_2").GetComponentInChildren<Renderer>();
        myLife[2] = GameObject.Find("holderUI_3").GetComponentInChildren<Renderer>();
        if(power_ups_selector.powerup_Enabled[2] == false)
        {
            for (int i = 0; i < myLife.Length; i++)
            {
                myLife[i].enabled = false;
            }
            return;
        }

        if (lifeCount == 3)
        {
            myLife[0].material.SetColor("_Color", Color.white);
            myLife[1].material.SetColor("_Color", Color.white);
            myLife[2].material.SetColor("_Color", Color.white);
        } else if (lifeCount == 2)
        {
            myLife[0].material.SetColor("_Color", Color.white);
            myLife[1].material.SetColor("_Color", Color.white);
            myLife[2].material.SetColor("_Color", Color.gray);
        } else if (lifeCount == 1)
        {
            myLife[0].material.SetColor("_Color", Color.white);
            myLife[1].material.SetColor("_Color", Color.gray);
            myLife[2].material.SetColor("_Color", Color.gray);
        } else if (lifeCount == 0)
        {
            myLife[0].material.SetColor("_Color", Color.gray);
            myLife[1].material.SetColor("_Color", Color.gray);
            myLife[2].material.SetColor("_Color", Color.gray);
        }
    }
}