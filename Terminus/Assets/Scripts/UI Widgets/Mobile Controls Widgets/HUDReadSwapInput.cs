using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Reads pointer-based input from quick select HUD, 
/// initiating weapon swapping in correct direction.
/// Used for mobile controls.
/// </summary>
public class HUDReadSwapInput : MonoBehaviour
{
    // event support
    HUDSwapWeaponEvent readInput;

    /// <summary>
    /// Used for late initialization
    /// </summary>
    void Start()
    {
        // add self as invoker of read swap input event
        readInput = new HUDSwapWeaponEvent();
        EventManager.AddHUDSwapWeaponInvoker(this);
    }

    /// <summary>
    /// Adds given method as listener to read swap input event
    /// </summary>
    /// <param name="newListener">new listener to event</param>
    public void AddReadSwapListener(UnityAction<bool> newListener)
    {
        readInput.AddListener(newListener);
    }
}
