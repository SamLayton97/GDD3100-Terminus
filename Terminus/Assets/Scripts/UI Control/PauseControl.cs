using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // event support
    TogglePauseEvent togglePauseEvent;

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // add self as invoker of toggle pause event
        togglePauseEvent = new TogglePauseEvent();
        EventManager.AddTogglePauseInvoker(this);
    }

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

    #endregion

    #region Public Methods

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
        // remove pause menu and display instructions
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    /// <summary>
    /// Handles when user clicks the "Close" button on
    /// the in-game instructions page
    /// </summary>
    public void HandleCloseInstructionsOnClick()
    {
        // remove instructions and display pause menu
        instructionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Handles when user clicks the "Exit" button
    /// </summary>
    public void HandleExitOnClick()
    {
        // return to main menu
        transitionSceneEvent.Invoke(transitionTo[0]);
    }

    /// <summary>
    /// Adds given method as listener of toggle pause event
    /// </summary>
    /// <param name="newListener">listener of event</param>
    public void AddTogglePauseListener(UnityAction<bool> newListener)
    {
        togglePauseEvent.AddListener(newListener);
    }

    #endregion

}
