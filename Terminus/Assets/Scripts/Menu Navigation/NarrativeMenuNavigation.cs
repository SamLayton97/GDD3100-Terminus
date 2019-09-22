using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls navigation between scenes from
/// the narrative-driven menus, typically moving
/// forward to something or back.
/// </summary>
public class NarrativeMenuNavigation : SceneTransitioner
{
    /// <summary>
    /// Handles when player clicks the "Return" button
    /// </summary>
    public void ReturnButtonOnClick()
    {
        // move back a scene
        transitionSceneEvent.Invoke(transitionTo[0]);
    }

    /// <summary>
    /// Handles when player clicks the "Proceed" button
    /// </summary>
    public void ProceedButtonOnClick()
    {
        // move forward a scene
        transitionSceneEvent.Invoke(transitionTo[1]);
    }
}
