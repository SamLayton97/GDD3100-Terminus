using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Facilitates adding invokers and listeners to various in-game events
/// </summary>
public static class EventManager
{
    #region Update O2 Gauge

    // declare lists to hold invokers and listeners to update O2 display event
    static List<OxygenControl> updateO2Invokers = new List<OxygenControl>();
    static List<UnityAction<float>> updateO2Listeners = new List<UnityAction<float>>();

    // Adds given oxygen controller as invoker of update O2 event
    public static void AddUpdateO2Invoker(OxygenControl invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        updateO2Invokers.Add(invoker);
        foreach (UnityAction<float> listener in updateO2Listeners)
            invoker.AddUpdateO2Listener(listener);
    }

    // Adds given method as listener to update O2 event
    public static void AddUpdateO2Listener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        updateO2Listeners.Add(listener);
        foreach (OxygenControl invoker in updateO2Invokers)
            invoker.AddUpdateO2Listener(listener);
    }

    #endregion

}
