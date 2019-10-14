/// <summary>
/// 10/14/19 
/// Allan Murillo   
/// Third Person Controller Project
/// MainMenu.cs
/// </summary>
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Load Game Level
    /// </summary>
    public void PlayGame()
    {
        // Debug.Log("Playing Level 1");
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Open Settings window
    /// </summary>
    public void ToSettings()
    {
        Debug.Log("Settings");
    }

    /// <summary>
    /// Load Main Menu
    /// </summary>
    public void ToMainMenu()
    {
        Debug.Log("Exiting Level");
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void Quit()
    {
        Debug.Log("Quitting Game");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}