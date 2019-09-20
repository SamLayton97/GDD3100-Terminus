using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages oxygen depletion and re-gain of agent, including instances of player death
/// </summary>
public class OxygenControl : SceneTransitioner
{
    // private variables
    int maxOxygen = 100;                    // total capacity of agent's oxygen tank
    float currOxygen = 0;                   // remaining percentage of agent's oxygen tank

    // public variables
    public float oxygenDepletionRate = 1f;  // percent of oxygen used per second

    // event support
    UpdateO2DisplayEvent updateO2Event;    // event invoked to update player's oxygen on UI

    #region Unity Methods

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // add self as invoker appropriate events
        updateO2Event = new UpdateO2DisplayEvent();
        EventManager.AddUpdateO2Invoker(this);

        //  add self as listener to relevant events
        EventManager.AddRefillO2Listener(RefillO2Tank);
        EventManager.AddDeductO2Listener(EmptyO2Tank);

        // "fill" agent's oxygen tank to capacity
        currOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        // reduce oxygen by rate * time
        EmptyO2Tank(oxygenDepletionRate * Time.deltaTime);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Refills oxygen tank by set amount, maxing out at tank capacity
    /// </summary>
    /// <param name="amountRefilled">amount of O2 filled</param>
    void RefillO2Tank(float amountRefilled)
    {
        currOxygen = Mathf.Min(maxOxygen, currOxygen + amountRefilled);
    }

    /// <summary>
    /// Empties oxygen tank by set amount, stopping at fully empty tank
    /// (i.e., death)
    /// </summary>
    /// <param name="amountEmptied">amount of O2 emptied</param>
    void EmptyO2Tank(float amountEmptied)
    {
        // reduce oxygen by amount, killing player if remaining O2 hits 0
        currOxygen = Mathf.Max(0, currOxygen - amountEmptied);
        if (currOxygen <= 0) KillPlayer();

        // update O2 display
        updateO2Event.Invoke(currOxygen);
    }

    /// <summary>
    /// Initiates player death-related events
    /// </summary>
    void KillPlayer()
    {
        // elect to transition to player death scene
        transitionSceneEvent.Invoke(transitionTo);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given listener to Update O2 display event
    /// </summary>
    /// <param name="newListener">new listener for event</param>
    public void AddUpdateO2Listener(UnityAction<float> newListener)
    {
        updateO2Event.AddListener(newListener);
    }

    #endregion
    
}
