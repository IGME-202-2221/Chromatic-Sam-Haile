using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI playText, controlsText, quitText, backText;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
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
