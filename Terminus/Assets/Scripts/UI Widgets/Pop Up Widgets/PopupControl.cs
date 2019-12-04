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
    // configuration variables
    public KeyCode pauseKey = KeyCode.Escape;               // key to pause/unpause game
    public GameObject darkenGameOnPause;                    // semi-transparent panel covering game when paused
    public AudioClipNames myPauseSound =                    // sound played when user pauses game
        AudioClipNames.UI_gamePause;
    public AudioClipNames myUnpauseSound =                  // sound played when user unpauses game
        AudioClipNames.UI_gameUnpause;

    // pop-up menu support variables
    [SerializeField] ShowHidePopup pauseMenuControl;
    [SerializeField] ShowHidePopup optionsMenuControl;
    [SerializeField] ShowHidePopup levelEndMenuControl;

    // crafting menu support variables
    [SerializeField] ShowHidePopup inventoryController;
    [SerializeField] ShowHidePopup craftingMenuController;

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
    HideMobileControlsEvent hideControlsEvent;

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

        // add self as invoker of appropriate events
        togglePauseEvent = new TogglePauseEvent();
        EventManager.AddTogglePauseInvoker(this);
        hideControlsEvent = new HideMobileControlsEvent();
        EventManager.AddMobileControlsInvoker(this);

        // add self as listener for relevant events
        EventManager.AddEndLevelListener(HandleEndLevel);
        EventManager.AddDetectSwipeListener(ControlByGesture);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause Control
        // if user attempts to pause game, game is not already paused, and level hasn't ended
        if (Input.GetKeyDown(pauseKey) && Time.timeScale != 0 && !levelEndMenuControl.Shown)
        {
            // open pause menu
            OpenPause();
        }
        // but if user attempts to unpause game, game is paused, and level hasn't ended
        else if (Input.GetKeyDown(pauseKey) && Time.timeScale == 0 && !levelEndMenuControl.Shown)
        {
            // close pause menu
            ClosePause();
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
                OpenCraftingMenu();
            }
            // but if user attempts to close crafting menu and game is paused
            else if (((!buttonInput && CustomInputManager.GetKeyDown("ShowHideCraftingMenu"))
                    || (buttonInput && CustomInputManager.GetMouseButtonDown("ShowHideCraftingMenu"))) && Time.timeScale == 0)
            {
                CloseCraftingMenu();
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

    /// <summary>
    /// Adds given method as listener of show/hide mobile controls event
    /// </summary>
    /// <param name="newListener">listener of event</param>
    public void AddShowControlsListener(UnityAction<bool> newListener)
    {
        hideControlsEvent.AddListener(newListener);
    }

    #endregion

    #region Private Methods

    #region Event Responders

    /// <summary>
    /// Controls opening and closing of menus 
    /// through swipe gestures
    /// </summary>
    /// <param name="direction"></param>
    void ControlByGesture(SwipeDirection direction)
    {
        // pause game on swipes down
        if (direction == SwipeDirection.Down)
            OpenPause();
        // open crafting menu on swipes up
        else if (direction == SwipeDirection.Up)
            OpenCraftingMenu();
    }

    /// <summary>
    /// Handles when player has ended a level in success or failure
    /// </summary>
    /// <param name="endedInSuccess">flag determining success</param>
    /// <param name="remainingSanity">player's sanity at end of level</param>
    void HandleEndLevel(bool endedInSuccess, float remainingSanity)
    {
        // hide UI elements, only freezing game on success
        hideControlsEvent.Invoke(true);
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

    #region Menu Visibility Control

    /// <summary>
    /// Opens pause menu, pausing game and hiding any
    /// visible HUD elements
    /// </summary>
    void OpenPause()
    {
        // pause game and hide mobile controls
        Time.timeScale = 0;
        togglePauseEvent.Invoke(true);
        hideControlsEvent.Invoke(true);
        AudioManager.Play(myPauseSound, true);
        CursorManager.Instance.HandlePause(true);

        // show pause menu
        darkenGameOnPause.SetActive(true);
        pauseMenuControl.ToggleDisplay(true);
    }

    /// <summary>
    /// Closes pause menu, resuming game and revealing
    /// any suspended HUD elements
    /// </summary>
    void ClosePause()
    {
        // hide pause menu and options
        darkenGameOnPause.SetActive(false);
        pauseMenuControl.ToggleDisplay(false);
        optionsMenuControl.ToggleDisplay(false);

        // hide crafting menu components
        craftingMenuController.ToggleDisplay(false);
        inventoryController.ToggleDisplay(false);

        // unpause game and show mobile controls
        Time.timeScale = 1;
        togglePauseEvent.Invoke(false);
        hideControlsEvent.Invoke(false);
        AudioManager.Play(myUnpauseSound, true);
        CursorManager.Instance.HandlePause(false);
    }

    /// <summary>
    /// Opens crafting menu, pausing game and hiding mobile controls
    /// </summary>
    void OpenCraftingMenu()
    {
        // pause game and hide mobile controls
        Time.timeScale = 0;
        AudioManager.Play(myPauseSound, true);
        CursorManager.Instance.HandlePause(true);
        hideControlsEvent.Invoke(true);

        // reveal materials inventory and crafting menu
        inventoryController.ToggleDisplay(true);
        craftingMenuController.ToggleDisplay(true);
    }

    /// <summary>
    /// Closes crafting menu, resuming game and revealing mobile controls
    /// </summary>
    void CloseCraftingMenu()
    {
        // unpause game and show mobile controls
        Time.timeScale = 1;
        AudioManager.Play(myUnpauseSound, true);
        CursorManager.Instance.HandlePause(false);
        hideControlsEvent.Invoke(false);

        // hide materials inventory and crafting menu
        inventoryController.ToggleDisplay(false);
        craftingMenuController.ToggleDisplay(false);
    }

    #endregion

    #endregion

}
