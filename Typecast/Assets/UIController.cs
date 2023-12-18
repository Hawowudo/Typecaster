using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject enemySpawnerObject;
    public GameObject foodBalanceGameObject;
    public GameObject startUI;
    public GameObject gameoverScreen;
    public void EnableStartUI()
    {
        startUI.SetActive(true);
        foodBalanceGameObject.SetActive(false);
        enemySpawnerObject.SetActive(false);
        FindAnyObjectByType<TimerBehavior>().PauseTime();
    }
    public void DIsableStartUI()
    {
        startUI.SetActive(false);
        foodBalanceGameObject.SetActive(true);
        enemySpawnerObject.SetActive(true);
        FindAnyObjectByType<TimerBehavior>().ResumeTime();
    }
    public void enableGameOver()
    {
        FindAnyObjectByType<TimerBehavior>().PauseTime();
        gameoverScreen.SetActive(true);
    }
    public void disableGameOver() 
    {
        gameoverScreen.SetActive(false);
    }
    
}
