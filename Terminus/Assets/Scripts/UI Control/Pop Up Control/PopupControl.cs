using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Pauses/unpauses game on user input/level completion events
/// </summary>
[RequireComponent(typeof(EndOfLevelEvaluator))]
public class PopupControl : SceneTransitioner
{
    // public variables
    public KeyCode pauseKey = KeyCode.Escape;               // key to pause/unpause game
    public GameObject darkenGameOnPause;                    // semi-transparent panel covering game when paused
    public GameObject pauseMenu;                            // pop-up pause menu
    public GameObject instructionsMenu;                     // in-game controls menu
    public GameObject endOfLevelMenu;                       // menu displayed when user completes a level
    public CanvasGroup materialsInventory;                  // canvas group component of inventory containing crafting materials player has collected
    public AudioClipNames myPauseSound =                    // sound played when user pauses game
        AudioClipNames.UI_gamePause;
    public AudioClipNames myUnpauseSound =                  // sound played when user unpauses game
        AudioClipNames.UI_gameUnpause;

    // end-of-level component variables
    public Text endOfLevelStatus;                           // text displaying whether user successfully ended level
    public string endedInSuccessText = "Level Complete";    // text displayed when user completes level
    public Color successTextColor;                          // color of text when user completes level
    public string endedInFailureText = "You Died";          // text displayed when user fails level
    public Color failureTextColor;                          // color of text when user fails level

    // private variables
    EndOfLevelEvaluator myEoLEvaluator;                 // component used to evaluate and represent player's performance

    // event support
    TogglePauseEvent togglePauseEvent;

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        myEoLEvaluator = GetComponent<EndOfLevelEvaluator>();
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // add self as invoker of toggle pause event
        togglePauseEvent = new TogglePauseEvent();
        EventManager.AddTogglePauseInvoker(this);

        // add self as listener for end level event
        EventManager.AddEndLevelListener(HandleEndLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause Control
        // if user attempts to pause game and game is not already paused
        if (Input.GetKeyDown(pauseKey) && Time.timeScale != 0)
        {
            // pause game
            Time.timeScale = 0;
            togglePauseEvent.Invoke(true);
            AudioManager.Play(myPauseSound, true);

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

            // unpause game
            Time.timeScale = 1;
            togglePauseEvent.Invoke(false);
            AudioManager.Play(myUnpauseSound, true);
        }

        // Crafting Menu Control
        // if no other pause-controlling UI elements are active
        if (!(pauseMenu.activeSelf || instructionsMenu.activeSelf || endOfLevelMenu.activeSelf))
        {
            // if user attempts to bring up crafting menu and game isn't paused
            if (Input.GetButtonDown("ShowHideCraftingMenu") && Time.timeScale != 0)
            {
                // pause game
                Time.timeScale = 0;
                AudioManager.Play(myPauseSound, true);

                // reveal materials inventory
                materialsInventory.alpha = 1;
                materialsInventory.blocksRaycasts = true;
            }
            // but if user attempts to close crafting menu and game is paused
            else if (Input.GetButtonDown("ShowHideCraftingMenu") && Time.timeScale == 0)
            {
                // pause game
                Time.timeScale = 1;
                AudioManager.Play(myPauseSound, true);

                // hide materials inventory
                materialsInventory.alpha = 0;
                materialsInventory.blocksRaycasts = false;
            }
        }
    }

    #endregion

    #region Public Methods

    #region Pause Menu Methods

    /// <summary>
    /// Handles when user clicks the "Resume" button
    /// </summary>
    public void HandleResumeOnClick()
    {
        // disable pause menu components
        darkenGameOnPause.SetActive(false);
        pauseMenu.SetActive(false);

        // play button press sound
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);

        // unpause game
        Time.timeScale = 1;
        togglePauseEvent.Invoke(false);
        AudioManager.Play(myUnpauseSound, true);
    }

    /// <summary>
    /// Handles when user clicks the "Controls"/"Help" button
    /// </summary>
    public void HandleControlsOnClick()
    {
        // remove pause menu and display instructions
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(true);

        // play button press sound
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
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

        // play button press sound
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Handles when user clicks the "Exit" button
    /// </summary>
    public void HandleExitOnClick()
    {
        // return to main menu
        transitionSceneEvent.Invoke(transitionTo[0]);
        Time.timeScale = 1;

        // play button press sound
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    #endregion

    #region End Of Level Menu Buttons

    /// <summary>
    /// Handles when user clicks retry button
    /// </summary>
    public void HandleRetryOnClick()
    {
        // reload current scene
        transitionSceneEvent.Invoke(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

        // play button press sound
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    #endregion

    /// <summary>
    /// Adds given method as listener of toggle pause event
    /// </summary>
    /// <param name="newListener">listener of event</param>
    public void AddTogglePauseListener(UnityAction<bool> newListener)
    {
        togglePauseEvent.AddListener(newListener);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles when player has ended a level in success or failure
    /// </summary>
    /// <param name="endedInSuccess">flag determining success</param>
    /// <param name="remainingSanity">player's sanity at end of level</param>
    void HandleEndLevel(bool endedInSuccess, float remainingSanity)
    {
        // hide UI elements, only freezing game on success
        togglePauseEvent.Invoke(true);
        if (endedInSuccess)
            Time.timeScale = 0;

        // enable end-of-level menu components
        darkenGameOnPause.SetActive(true);
        endOfLevelMenu.SetActive(true);

        // set end-of-level popup components to reflect player's success status
        endOfLevelStatus.text = (endedInSuccess ? endedInSuccessText : endedInFailureText);
        endOfLevelStatus.color = (endedInSuccess ? successTextColor : failureTextColor);

        // evaluate player's performance
        myEoLEvaluator.Evaluate(remainingSanity);
    }

    #endregion

}
