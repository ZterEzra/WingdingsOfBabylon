using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the level you want to play on button click
        SceneManager.LoadScene("Level1"); // Replace with your level name
    }

    public void QuitGame()
    {
        // Quit the application on button click (works in builds, not editor)
        Application.Quit();
    }
}
