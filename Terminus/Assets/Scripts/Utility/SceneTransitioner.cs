using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of objects which facilitate transitions between scenes
/// (typically managers);
/// </summary>
public abstract class SceneTransitioner : MonoBehaviour
{
    // private variables
    [SerializeField] string transitionTo;   // name of scene which object can transition to

    /// <summary>
    /// Provides read-access to name of scene object can load.
    /// Accessible to children of this class.
    /// </summary>
    protected string TransitionTo
    {
        get { return transitionTo; }
    }
}
