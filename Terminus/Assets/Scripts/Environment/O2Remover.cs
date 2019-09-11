using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class of all objects which can take away from player's O2 pool
/// </summary>
public abstract class O2Remover : MonoBehaviour
{
    // event support
    DeductPlayerO2Event deductO2Event;

    // Start is called before the first frame update
    protected void Start()
    {
        // add self as invoker of deduct player health event
        deductO2Event = new DeductPlayerO2Event();
        EventManager.AddDeductO2Invoker(this);
    }

    /// <summary>
    /// Adds given method as listener to deduct player O2 event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddDeductO2Listener(UnityAction<float> newListener)
    {
        deductO2Event.AddListener(newListener);
    }
}
