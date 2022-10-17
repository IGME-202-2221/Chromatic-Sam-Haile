using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public ScoreScript scoreScript;
    public TextMeshProUGUI restartText, quitText;

    public void Restart()
    {
        ScoreScript.scoreNum = 0;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeColorCyan(TextMeshProUGUI text)
    {
        text.color = Color.cyan;
    }

    public void ChangeColorWhite(TextMeshProUGUI text)
    {
        text.color = Color.white;
    }
}
