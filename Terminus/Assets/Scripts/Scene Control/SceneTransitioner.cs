using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Base class of objects which facilitate transitions between scenes
/// (typically managers);
/// </summary>
public abstract class SceneTransitioner : MonoBehaviour
{
    // public variables
    public Scene transitionTo;      // scene which object can transition to

    /// <summary>
    /// Provides read-access to name of scene object can load.
    /// Accessible to children of this class.
    /// </summary>
    protected string TransitionTo
    {
        get { return transitionTo.name; }
    }
}
