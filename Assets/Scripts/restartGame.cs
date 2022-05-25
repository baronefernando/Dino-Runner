using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restartGame : MonoBehaviour
{
    Text score;
    Text HighScore;

    Text coinsEarned;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("myScore").GetComponentInChildren<Text>();
        HighScore = GameObject.Find("myHighScore").GetComponentInChildren<Text>();
        coinsEarned = GameObject.Find("myCoinsEarned").GetComponentInChildren<Text>();
        score.text = "SCORE: " + scoreScript.scoreValue;
        HighScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("hscore");

        coinsEarned.text = "$" + earnedCoins();
        for (int i = 0; i < power_ups_selector.powerup_Enabled.Length; i++)
        {
            power_ups_selector.powerup_Enabled[i] = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public int earnedCoins()
    {
        int earned = 0;
        int currentCoins = 0;
        currentCoins = PlayerPrefs.GetInt("coins");
        earned = scoreScript.scoreValue / 5;
        currentCoins += earned;
        PlayerPrefs.SetInt("coins", currentCoins);
        return earned;
    }
}
