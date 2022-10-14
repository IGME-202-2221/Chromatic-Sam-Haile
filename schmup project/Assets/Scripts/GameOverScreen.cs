using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public ScoreScript scoreScript;

    public void Setup()
    {
        //gameObject.SetActive(true);
        //pointsText.text = scoreScript.gameOverScore.ToString();
    }

    public void Restart()
    {
        ScoreScript.scoreNum = 0;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }




}
