using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class power_ups_selector : MonoBehaviour
{
    public Image[] buttons;
    public Image[] choices;
    public Sprite[] spriteArray;
    public static bool[] powerup_Enabled = new bool[3];

    private Text coins;
    private int currency;

    int num;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("coins"))
        {
            currency = PlayerPrefs.GetInt("coins");
        }
        else
        {
            currency = 300;
            PlayerPrefs.SetInt("coins", currency);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coins = GameObject.Find("coins").GetComponentInChildren<Text>();
        coins.text = "$" + currency;
    }
    
    public void clearPicks(Sprite s)
    {
        for(int i = 0; i < 3; i++)
        {
            if(powerup_Enabled[i])
            {
                refund(i);
            }
            choices[i].sprite = s;
            powerup_Enabled[i] = false;
        }
    }

    private void refund(int c)
    {
        if(c == 0)
        {
            currency += 20;
        }
        else if(c == 1)
        {
            currency += 90;
        }
        else if(c == 2)
        {
            currency += 150;
        }
        PlayerPrefs.SetInt("coins", currency);

    }

    private bool chargePrice(int p)
    {
        int currencyOld = currency;

        if((currencyOld - p) > 0)
        {
            currency -= p;
            PlayerPrefs.SetInt("coins", currency);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeSprite(Image img)
    {
        int price = 0;
        if(img.sprite.name.Equals("powerups_0"))
        {
            num = 0;
            price = 20;

        }
        else if(img.sprite.name.Equals("powerups_1"))
        {
            num = 1;
            price = 90;
        }
        else if(img.sprite.name.Equals("powerups_2"))
        {
            num = 2;
            price = 150;
        }

        if(powerup_Enabled[num] == true)
        {
            return;
        }

        if(chargePrice(price) == true)
        {
            for(int i = 0; i < 3; i++)
            {  
                if(choices[i].sprite.name.Equals("blankChoice"))
                {
                    choices[i].sprite = spriteArray[num];
                    powerup_Enabled[num] = true;
                    break;
                }
            }
        }
    }
}
