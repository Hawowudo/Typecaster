using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadHighScores : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text.text = "High score: \n Enemies Defeated: " + PlayerPrefs.GetInt("enemDef") + " Seconds survived: " +PlayerPrefs.GetInt("secSur") ;
    }

    
}
