using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text myScoreText;
    public Text gameOverScore;
    public Text highscore;
    public static int scoreNum;
    public static int highscoreNum;

    void Update()
    {
        // print score on top right of window
        if (myScoreText != null)
        {
            myScoreText.text = "Score:  " + ScoreScript.scoreNum;
        }
        // prints score on game over screen
        if (gameOverScore != null)
        {
            int points = ScoreScript.scoreNum;
            gameOverScore.text = "Score: " + points.ToString();
        }
        // prints highscore
        if (highscore != null)
        {
            int highscoreNumber = ScoreScript.highscoreNum;
            highscore.text = "Highscore: " + highscoreNumber.ToString();
        }

    }
}
