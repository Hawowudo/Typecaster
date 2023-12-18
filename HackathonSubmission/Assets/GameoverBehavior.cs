using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverBehavior : MonoBehaviour
{
    public TextMeshProUGUI text;
    public EnemySpawner enemySpawner;
    public TimerBehavior timerBehavior;
    private void Start()
    {
        enemySpawner.gameObject.SetActive(false);
        text.text = "Enemies defeated: "+ enemySpawner.enemiesDefeated + ", Seconds survived: " + timerBehavior.seconds + " \n Gameover, press space to restart";
        Time.timeScale = 0f;
    }
    void Restart()
    {
        if(PlayerPrefs.GetInt("enemDef") < enemySpawner.enemiesDefeated && PlayerPrefs.GetInt("secSur") < timerBehavior.seconds)
        {
            PlayerPrefs.SetInt("enemDef", enemySpawner.enemiesDefeated);
            PlayerPrefs.SetInt("secSur", timerBehavior.seconds);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }
}
