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
    public GameObject instructionsMenu;                 // in-game controls menu

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
            instructionsMenu.SetActive(false);

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
        // remove pause menu and display control scheme
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    /// <summary>
    /// Handles when user clicks the "Exit" button
    /// </summary>
    public void HandleExitOnClick()
    {
        // return to main menu
        transitionSceneEvent.Invoke(transitionTo[0]);
    }
}
