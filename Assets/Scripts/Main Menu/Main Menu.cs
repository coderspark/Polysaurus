using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void PlayGame()
    {
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("Main Level");
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}
