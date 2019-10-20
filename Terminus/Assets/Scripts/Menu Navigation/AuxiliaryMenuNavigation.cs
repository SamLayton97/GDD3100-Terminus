using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls navigation between scenes from
/// the auxiliary menus such as the Controls
/// and Credits scenes.
/// </summary>
public class AuxiliaryMenuNavigation : SceneTransitioner
{
    /// <summary>
    /// Handles player pressing the "Return" button
    /// </summary>
    public void ReturnButtonOnClick()
    {
        // transition to menu before this (likely Title scene)
        transitionSceneEvent.Invoke(transitionTo[0]);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Handles player pressing the "Proceed" button
    /// </summary>
    public void ProceedButton()
    {
        // transition to menu after this
        transitionSceneEvent.Invoke(transitionTo[1]);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }
}
