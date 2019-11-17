using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class of all objects which can take away from player's sanity pool
/// </summary>
public abstract class SanityDeductor : MonoBehaviour
{
    // event support
    protected DeductSanityEvent deductSanityEvent;

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    protected virtual void Start()
    {
        // add self as invoker to deduct sanity event
        deductSanityEvent = new DeductSanityEvent();
        EventManager.AddDeductSanityInvoker(this);
    }

    /// <summary>
    /// Adds given method as listener to deduct sanity on fire event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddDeductListener(UnityAction<float> newListener)
    {
        deductSanityEvent.AddListener(newListener);
    }

}
