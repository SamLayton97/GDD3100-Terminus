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
    }
}
