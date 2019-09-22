using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Pauses game when user ends level either through death or
/// meeting their objective. Also, handles click events for
/// its buttons on popup.
/// </summary>
public class EndOfLevelControl : SceneTransitioner
{
    // public variables
    public GameObject darkenGameOnPause;                // semi-transparent panel covering game when paused
    public GameObject endOfLevelMenu;                   // menu shown when user ends a level


    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Handles when user clicks retry button
    /// </summary>
    public void HandleRetryOnClick()
    {
        // reload current scene
        transitionSceneEvent.Invoke(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Handles when user clicks proceed button
    /// </summary>
    public void HandleProceedOnClick()
    {
        // move forward to next pre-defined scene
        transitionSceneEvent.Invoke(transitionTo[0]);
    }
}
