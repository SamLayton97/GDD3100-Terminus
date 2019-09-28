using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

/// <summary>
/// Manages oxygen depletion and re-gain of agent, including instances of player death
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(FaceMousePosition))]
[RequireComponent(typeof(PlayerFire))]
[RequireComponent(typeof(CircleCollider2D))]
public class OxygenControl : LevelEnder
{
    // private variables
    int maxOxygen = 100;                    // total capacity of agent's oxygen tank
    float currOxygen = 0;                   // remaining percentage of agent's oxygen tank
    SpriteRenderer mySpriteRenderer;        // object's sprite renderer component (used to control color of sprite)
    FaceMousePosition myLook;               // player's look-input component (disabled on death)
    PlayerFire myFire;                      // player's combat component (disabled on death)
    CircleCollider2D myTriggerCollider;     // player's trigger collider component (disabled on death)

    // public variables
    public float oxygenDepletionRate = 1f;              // percent of oxygen used per second
    public Color deathColor;                            // color player's sprite transitions to on death
    public float screenShakeMagnitudeScalar = 0.05f;    // scale by which screen shakes according to damage taken by player
    public float screenShakeRoughness = 4f;             // how rough screen shake is
    public float screenShakeFadeInTime = 0.1f;          // time it takes for screen shake to reach peak magnitude
    public float screenShakeFadeOutTime = 0.1f;         // time it takes for screen shake to end

    // event support
    UpdateO2DisplayEvent updateO2Event;    // event invoked to update player's oxygen on UI

    /// <summary>
    /// Property with read-access returning amount of O2
    /// left in player's tank.
    /// </summary>
    public float CurrentOxygen
    {
        get { return currOxygen; }
    }

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myLook = GetComponent<FaceMousePosition>();
        myFire = GetComponent<PlayerFire>();
        myTriggerCollider = GetComponent<CircleCollider2D>();
    }

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
        updateO2Event.Invoke(currOxygen);
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

        // TODO: shake camera by how much damage player took
        CameraShaker.Instance.ShakeOnce(screenShakeMagnitudeScalar * amountEmptied, screenShakeRoughness, 
            screenShakeFadeInTime, screenShakeFadeOutTime);

        // update O2 display
        updateO2Event.Invoke(currOxygen);
    }

    /// <summary>
    /// Initiates player death-related events
    /// </summary>
    void KillPlayer()
    {
        // soft-disable player
        mySpriteRenderer.color = deathColor;
        myLook.enabled = false;
        myFire.enabled = false;
        myTriggerCollider.enabled = false;

        // invoking end level event with failure
        endLevelEvent.Invoke(false, 0);
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
