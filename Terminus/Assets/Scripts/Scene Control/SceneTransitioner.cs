using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class of objects which elect to transitions to a different scene.
/// (Usually objects controlling player success and death).
/// </summary>
public abstract class SceneTransitioner : MonoBehaviour
{
    // private variables
    [SerializeField] string transitionTo;   // name of scene which object can transition to

    // event support
    TransitionToSceneEvent transitionSceneEvent;

    /// <summary>
    /// Provides read-access to name of scene object can load.
    /// Accessible to children of this class.
    /// </summary>
    protected string TransitionTo
    {
        get { return transitionTo; }
    }

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        // Add self as invoker of transition scene event
        transitionSceneEvent = new TransitionToSceneEvent();
        EventManager.AddTransitionSceneInvoker(this);
    }

    /// <summary>
    /// Adds given method as listener to Transition Scene Event
    /// </summary>
    /// <param name="newListener">new listener of event</param>
    public void AddTransitionSceneListener(UnityAction<string> newListener)
    {
        transitionSceneEvent.AddListener(newListener);
    }
}
