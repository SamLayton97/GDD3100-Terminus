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
    static List<UnityAction<int>> updateO2Listeners = new List<UnityAction<int>>();

    // Adds given oxygen controller as invoker of update O2 event
    public static void AddUpdateO2Invoker(OxygenControl invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        updateO2Invokers.Add(invoker);
        foreach (UnityAction<int> listener in updateO2Listeners)
            invoker.AddUpdateO2Listener(listener);
    }

    // Adds given method as listener to update O2 event
    public static void AddUpdateO2Listener(UnityAction<int> listener)
    {
        // adds listener to list and to all invokers of event
        updateO2Listeners.Add(listener);
        foreach (OxygenControl invoker in updateO2Invokers)
            invoker.AddUpdateO2Listener(listener);
    }

    #endregion

    #region Refill Player O2

    // declare lists to hold invokers and listeners to refill player O2 event
    static List<RefillO2OnCollision> refillO2Invokers = new List<RefillO2OnCollision>();
    static List<UnityAction<float>> refillO2Listeners = new List<UnityAction<float>>();

    // Adds given O2 refiller as invoker of Refill Player O2 event
    public static void AddRefillO2Invoker(RefillO2OnCollision invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        refillO2Invokers.Add(invoker);
        foreach (UnityAction<float> listener in refillO2Listeners)
            invoker.AddRefillO2Listener(listener);
    }

    // Adds given method as listener to refill player O2 event
    public static void AddRefillO2Listener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        refillO2Listeners.Add(listener);
        foreach (RefillO2OnCollision invoker in refillO2Invokers)
            invoker.AddRefillO2Listener(listener);
    }

    #endregion
}
