using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hides/reveals UI element when player
/// pauses or unpauses game.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class HideOnPause : MonoBehaviour
{
    // private variables
    CanvasGroup myCanvasGroup;          // component used to control alpha value of UI element

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // add self as listener for toggle pause event
        EventManager.AddTogglePauseListener(ToggleVisibility);
    }

    /// <summary>
    /// Hide/reveal UI element when user pauses/unpauses game
    /// </summary>
    /// <param name="isPaused">whether user has paused game</param>
    void ToggleVisibility(bool isPaused)
    {
        // if user has paused game
        if (isPaused)
        {
            // hide element and prevent element from blocking raycasts
            myCanvasGroup.alpha = 0;
            myCanvasGroup.blocksRaycasts = false;
        }
        // otherwise
        else
        {
            // reveal element and enable it to block raycasts
            myCanvasGroup.alpha = 1;
            myCanvasGroup.blocksRaycasts = true;
        }
    }
}
