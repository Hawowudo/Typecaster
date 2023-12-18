using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputDetection : MonoBehaviour
{
    string text;
    public List<ButtonBehavior> ListOfAllButtons;
    public PlayerBehavior playerBehavior;
    public Animator playeranimator;
    public float cooldown;
    bool startUI;
    private void Start()
    {
        playerBehavior = FindAnyObjectByType<PlayerBehavior>();
    }
    private void Update()
    {
        GetUIList();
        GetInput();
        if (Input.GetKeyDown(KeyCode.Escape) && startUI != true)
        {
            startUI = true;
            FindAnyObjectByType<UIController>().EnableStartUI();
        } else if (Input.GetKeyDown(KeyCode.Escape) && startUI == true)
        {
            startUI = false;
            FindAnyObjectByType<UIController>().DIsableStartUI();

        }
    }
    private void GetInput()
    {
        if(Input.inputString != null && Input.inputString != "" && Input.inputString != " ")
        {
            cooldown = 1;
            text = Input.inputString.ToUpper();
            Debug.Log(text);
            ApplyDamage(text);
            StopCoroutine("IsTypingCooldown");
            playeranimator.SetBool("IsTyping", true);
            //get all enemies
            //apply damage
        }
        else
        {
            IsTypingCooldown();
        }
    }
    void IsTypingCooldown()
    {
        if(cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            playeranimator.SetBool("IsTyping", false);
        }
    }
    void GetUIList()
    {
        ListOfAllButtons.RemoveAll(obj => obj == null);
        ListOfAllButtons = FindObjectsOfType<ButtonBehavior>().ToList();
    }
    void ApplyDamage(string text)
    {
        
        if(ListOfAllButtons != null)
        {
            foreach(ButtonBehavior item in ListOfAllButtons)
            {
                item.RemoveCharacter(text);
            }
        }
        if(FindAnyObjectByType<EnemySpawner>() == null)
        {
            return;
        }
        foreach(EnemyBehavior item in FindAnyObjectByType<EnemySpawner>().GetListOfAllEnemies())
        {
            item.RemoveCharacter(
                text
                );
        }
    }
}
