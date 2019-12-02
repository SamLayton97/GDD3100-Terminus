using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hides/reveals UI element when player pauses/unpauses
/// game or opens/closes crafting menu
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class HideMobileControls : HideOnPause
{
    /// <summary>
    /// Start is called before the first frame of Update()
    /// </summary>
    protected override void Start()
    {
        // add self as listener for show/hide controls event
        EventManager.AddMobileControlsListener(ToggleVisibility);
    }

    /// <summary>
    /// Hide/reveal UI element when user pauses/unpauses game
    /// </summary>
    /// <param name="isPaused">whether user has paused game</param>
    protected override void ToggleVisibility(bool isPaused)
    {
        // if game is paused/menus are hidden
        if (isPaused)
        {
            // hide controls and disable interaction
            myCanvasGroup.alpha = 0;
            myCanvasGroup.blocksRaycasts = false;
            myCanvasGroup.interactable = false;
        }
        // otherwise (return to gameplay)
        else
        {
            // show controls and enable interaction
            myCanvasGroup.alpha = 1;
            myCanvasGroup.blocksRaycasts = true;
            myCanvasGroup.interactable = true;
        }
    }

}
