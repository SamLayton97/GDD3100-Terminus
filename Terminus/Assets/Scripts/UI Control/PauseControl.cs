using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pauses/unpauses game on user input
/// </summary>
public class PauseControl : SceneTransitioner
{
    // public variables
    public KeyCode pauseKey = KeyCode.Escape;           // key to pause/unpause game
    public GameObject darkenGameOnPause;                // semi-transparent panel covering game when paused
    public GameObject pauseMenu;                        // pop-up pause menu

    // Update is called once per frame
    void Update()
    {
        // if user attempts to pause game and game is not already paused
        if (Input.GetKeyDown(pauseKey) && Time.timeScale != 0)
        {
            // freeze game
            Time.timeScale = 0;

            // enable pause menu components
            darkenGameOnPause.SetActive(true);
            pauseMenu.SetActive(true);
        }
        // but if user attempts to unpause game and game is paused
        else if (Input.GetKeyDown(pauseKey) && Time.timeScale == 0)
        {
            // disable pause menu components
            darkenGameOnPause.SetActive(false);
            pauseMenu.SetActive(false);

            // unfreeze game
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Handles when user clicks the "Resume" button
    /// </summary>
    public void HandleResumeOnClick()
    {
        // disable pause menu components
        darkenGameOnPause.SetActive(false);
        pauseMenu.SetActive(false);

        // unfreeze game
        Time.timeScale = 1;
    }

    /// <summary>
    /// Handles when user clicks the "Controls"/"Help" button
    /// </summary>
    public void HandleControlsOnClick()
    {
        // TODO: remove pause menu and display control scheme
        Debug.Log("Display Controls");
    }

    /// <summary>
    /// Handles when user clicks the "Exit" button
    /// </summary>
    public void HandleExitOnClick()
    {
        // TODO: return to main menu
        Debug.Log("Return to main menu");
    }
}
