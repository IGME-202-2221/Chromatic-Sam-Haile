using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text myScoreText;
    public Text gameOverScore;
    public static int scoreNum;

    void Start()
    {
        scoreNum = 0;
    }

    void Update()
    {
        if (myScoreText != null)
        {
            myScoreText.text = "Score:  " + scoreNum;
        }
        else if (gameOverScore != null)
        {
            gameOverScore.text = "ASFDADSA" + scoreNum.ToString();
        }
    }
}
