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
    public GameObject instructionsMenu;                     // in-game instructions/options menu
    public GameObject endOfLevelMenu;                       // menu displayed when user completes a level
    public CanvasGroup materialsInventory;                  // canvas group component of inventory containing crafting materials player has collected
    public CanvasGroup craftingMenu;                        // canvas group of menu displayed along with inventory, allowing user to craft items
    public AudioClipNames myPauseSound =                    // sound played when user pauses game
        AudioClipNames.UI_gamePause;
    public AudioClipNames myUnpauseSound =                  // sound played when user unpauses game
        AudioClipNames.UI_gameUnpause;

    // pop-up menus
    [SerializeField] ShowHidePopup pauseMenuControl;
    [SerializeField] ShowHidePopup optionsMenuControl;
    [SerializeField] ShowHidePopup levelEndMenuControl;

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
        // retrieve relevant components
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
            CursorManager.Instance.HandlePause(true);

            // show pause menu
            darkenGameOnPause.SetActive(true);
            pauseMenuControl.ToggleDisplay(true);
        }
        // but if user attempts to unpause game and game is paused
        else if (Input.GetKeyDown(pauseKey) && Time.timeScale == 0)
        {
            // hide pause menu and options
            darkenGameOnPause.SetActive(false);
            pauseMenuControl.ToggleDisplay(false);
            optionsMenuControl.ToggleDisplay(false);

            // unpause game
            Time.timeScale = 1;
            togglePauseEvent.Invoke(false);
            AudioManager.Play(myUnpauseSound, true);
            CursorManager.Instance.HandlePause(false);
        }

        // Crafting Menu Control
        // if no other pause-controlling UI elements are visible
        if (!(pauseMenuControl.Shown || optionsMenuControl.Shown || levelEndMenuControl.Shown))
        {
            // determine whether to read button or key
            bool buttonInput = !ControlSchemeManager.UsingSpecialist;

            // if user attempts to bring up crafting menu and game isn't paused
            if (((!buttonInput && CustomInputManager.GetKeyDown("ShowHideCraftingMenu"))
                || (buttonInput && CustomInputManager.GetMouseButtonDown("ShowHideCraftingMenu"))) && Time.timeScale != 0)
            {
                // pause game
                Time.timeScale = 0;
                AudioManager.Play(myPauseSound, true);
                CursorManager.Instance.HandlePause(true);

                // reveal materials inventory and crafting menu
                materialsInventory.alpha = 1;
                materialsInventory.blocksRaycasts = true;
                craftingMenu.alpha = 1;
                craftingMenu.blocksRaycasts = true;
            }
            // but if user attempts to close crafting menu and game is paused
            else if (((!buttonInput && CustomInputManager.GetKeyDown("ShowHideCraftingMenu"))
                    || (buttonInput && CustomInputManager.GetMouseButtonDown("ShowHideCraftingMenu"))) && Time.timeScale == 0)
            {
                // pause game
                Time.timeScale = 1;
                AudioManager.Play(myUnpauseSound, true);
                CursorManager.Instance.HandlePause(false);

                // hide materials inventory and crafting menu
                materialsInventory.alpha = 0;
                materialsInventory.blocksRaycasts = false;
                craftingMenu.alpha = 0;
                craftingMenu.blocksRaycasts = false;
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
        // hide pause menu
        darkenGameOnPause.SetActive(false);
        pauseMenuControl.ToggleDisplay(false);

        // play button press sound
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);

        // unpause game
        Time.timeScale = 1;
        togglePauseEvent.Invoke(false);
        AudioManager.Play(myUnpauseSound, true);
        CursorManager.Instance.HandlePause(false);
    }

    /// <summary>
    /// Handles when user clicks the "Controls"/"Help" button
    /// </summary>
    public void HandleControlsOnClick()
    {
        // remove pause menu and display instructions
        pauseMenuControl.ToggleDisplay(false);
        optionsMenuControl.ToggleDisplay(true);

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
        optionsMenuControl.ToggleDisplay(false);
        pauseMenuControl.ToggleDisplay(true);

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

        // set player's mouse cursor
        CursorManager.Instance.SetCursorType(Cursors.Standard);

        // enable end-of-level menu components
        levelEndMenuControl.ToggleDisplay(true);
        darkenGameOnPause.SetActive(true);

        // set end-of-level popup components to reflect player's success status
        endOfLevelStatus.text = (endedInSuccess ? endedInSuccessText : endedInFailureText);
        endOfLevelStatus.color = (endedInSuccess ? successTextColor : failureTextColor);

        // evaluate player's performance
        myEoLEvaluator.Evaluate(remainingSanity);
    }

    #endregion

}
