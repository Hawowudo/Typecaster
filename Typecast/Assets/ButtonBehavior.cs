using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public enum ButtonType
    {
        start,
        exit
    }
    public ButtonType type; 
    string baseText;
    string currentText;
    char[] currentTextArray;
    public TextMeshProUGUI text;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        if(type == ButtonType.start)
        {
            baseText = "Start";
        }
        else
        {
            baseText = "Exit";
        }
        ResetCurrentText();
    }

    void SetText()
    {
        text.text = currentText;
    }
    // Update is called once per frame
    void Update()
    {
        SetText();
    }
    void ResetCurrentText()
    {
        currentText = baseText.ToUpper();
        currentTextArray = currentText.ToCharArray();
    }

    public void RemoveCharacter(string text)
    {
        if(text == null)
        {
            return;
        }
        if (currentTextArray[0].ToString() == text.ToUpper())
        {
            currentText = currentText.Remove(0, 1); 
            currentTextArray = currentText.ToUpper().ToCharArray();
            if(currentTextArray.Length <= 0)
            {
                if(type == ButtonType.exit)
                {
                    Application.Quit();
                }
                else if ( type == ButtonType.start)
                {

                    FindAnyObjectByType<UIController>().DIsableStartUI();
                }
            }
        }
        else
        {
            ResetCurrentText();
        }
    }
    void StartGame()
    {
      
    }
}
