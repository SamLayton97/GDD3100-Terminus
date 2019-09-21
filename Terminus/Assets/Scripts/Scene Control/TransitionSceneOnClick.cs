using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Elects to transition to specified scene when user clicks object.
/// Typically used by scene-transitioning UI buttons.
/// </summary>
public class TransitionSceneOnClick : SceneTransitioner
{
    /// <summary>
    /// Invokes transition scene event when clicked
    /// </summary>
    public void HandleClickEvent()
    {
        transitionSceneEvent.Invoke(transitionTo[0]);
    }
}
