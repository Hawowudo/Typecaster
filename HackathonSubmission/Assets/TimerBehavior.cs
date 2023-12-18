using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    EnemySpawner spawner;
    TextMeshProUGUI text;
    public int seconds;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void StartTime()
    {
        seconds = 0;
        InvokeRepeating("AddTime", 0, 1);
    }
    public void PauseTime()
    {
        CancelInvoke("AddTime");
    }
    public void ResumeTime()
    {
        if(seconds <= 0) 
        {
            seconds = 0;
        } 
        InvokeRepeating("AddTime", 0, 1);
    }

    void AddTime()
    {
        seconds++;
        text.text = seconds.ToString();
        if (seconds%30 == 0)
        {
            spawner.maxEnemyCount++;
            if(spawner.currentInterval <= 0.5)
            {
                return;
            }
            spawner.SpawnEnemiesAtAnInterval(spawner.currentInterval - 0.2f);
        }
    }
}
