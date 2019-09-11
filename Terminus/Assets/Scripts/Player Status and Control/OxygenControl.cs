using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages oxygen depletion and re-gain of agent
/// </summary>
public class OxygenControl : MonoBehaviour
{
    // private variables
    int maxOxygen = 100;                    // total capacity of agent's oxygen tank
    float currOxygen = 0;                   // remaining percentage of agent's oxygen tank

    // public variables
    public float oxygenDepletionRate = 1f;  // percent of oxygen used per second

    // event support
    UpdateO2DisplayEvent updateO2Event;    // event invoked to update player's oxygen on UI

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker to update O2 display event
        updateO2Event = new UpdateO2DisplayEvent();
        EventManager.AddUpdateO2Invoker(this);

        //  add self as listener to refill O2 event
        EventManager.AddRefillO2Listener(RefillO2Tank);

        // "fill" agent's oxygen tank to capacity
        currOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        // save oxygen from previous frame and reduce oxygen by rate * time
        int prevOxygen = (int)currOxygen;
        currOxygen = Mathf.Max(0, currOxygen - (oxygenDepletionRate * Time.deltaTime));

        // update oxygen display if rounded amount changed
        if ((int)currOxygen != prevOxygen)
            updateO2Event.Invoke((int)currOxygen);
    }

    /// <summary>
    /// Refills oxygen tank by set amount, maxing out at tank capacity
    /// </summary>
    /// <param name="amountRefilled">amount of O2 filled</param>
    void RefillO2Tank(float amountRefilled)
    {
        currOxygen = Mathf.Min(maxOxygen, currOxygen + amountRefilled);
    }

    /// <summary>
    /// Adds given listener to Update O2 display event
    /// </summary>
    /// <param name="newListener">new listener for event</param>
    public void AddUpdateO2Listener(UnityAction<int> newListener)
    {
        updateO2Event.AddListener(newListener);
    }
}
