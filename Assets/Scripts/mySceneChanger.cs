using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mySceneChanger : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void loadPowerUpChoice()
    {
        SceneManager.LoadScene("selectPowerUps");
    }
    public void loadMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void loadTutorial()
    {
        SceneManager.LoadScene("Tutorial_1");
    }
    public void loadTutorial2()
    {
        SceneManager.LoadScene("Tutorial_2");
    }
    public void loadSettings()
    {
        SceneManager.LoadScene("mySettings");
    }
    public void appQuit()
    {
        Application.Quit();
    }

    public void loadDonate()
    {
        Application.OpenURL("https://www.paypal.me/enorabf");
    }

}
