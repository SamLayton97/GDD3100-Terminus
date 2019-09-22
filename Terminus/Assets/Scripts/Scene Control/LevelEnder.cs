using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Abstract parent class of all objects capable of
/// ending a level in success or failure.
/// </summary>
public abstract class LevelEnder : MonoBehaviour
{
    // event support
    EndLevelEvent endLevelEvent;

    /// <summary>
    /// Called before first frame Update
    /// </summary>
    protected void Start()
    {
        // add self as invoker of End Level event
        endLevelEvent = new EndLevelEvent();
        EventManager.AddEndLevelInvoker(this);
    }

    /// <summary>
    /// Adds given listener to End Level event
    /// </summary>
    /// <param name="newListener">new method listening for event</param>
    public void AddEndLevelListener(UnityAction<bool, float> newListener)
    {
        endLevelEvent.AddListener(newListener);
    }
}
