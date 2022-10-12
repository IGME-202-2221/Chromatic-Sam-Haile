using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text myScoreText;
    public int scoreNum;

    void Start()
    {
        scoreNum = 0;
        
    }

    void Update()
    {
        myScoreText.text = "Score:  " + scoreNum;
    }
}
