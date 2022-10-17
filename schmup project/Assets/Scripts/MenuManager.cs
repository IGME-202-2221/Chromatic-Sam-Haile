using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text pointsText;
    public ScoreScript scoreScript;
    public TextMeshProUGUI playText, controlsText, quitText, backText, restartText, quitTextTwo;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        ScoreScript.scoreNum = 0;
        SceneManager.LoadScene(1);
    }

    public void MainMenuScreen()
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
