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
[RequireComponent(typeof(AudioSource))]
public class OxygenControl : LevelEnder
{
    // private variables
    int maxOxygen = 100;                    // total capacity of agent's oxygen tank
    float currOxygen = 0;                   // remaining percentage of agent's oxygen tank
    bool softDisabled = false;              // flag determining whether player object has been disabled (used for handling death)
    SpriteRenderer mySpriteRenderer;        // object's sprite renderer component (used to control color of sprite)
    FaceMousePosition myLook;               // player's look-input component (disabled on death)
    PlayerFire myFire;                      // player's combat component (disabled on death)
    CircleCollider2D myTriggerCollider;     // player's trigger collider component (disabled on death)
    AudioSource myBreathingSource;          // audio source used to play looping breathing effect

    // public variables
    public AudioClipNames[] myHurtSounds =              // sound effects played when player is hurt
        {
        AudioClipNames.player_hurt,
        AudioClipNames.player_hurt1,
        AudioClipNames.player_hurt2
        };
    public AudioClipNames myDeathSound =                // sound effect played when player dies
        AudioClipNames.player_death;
    public float oxygenDepletionRate = 1f;              // percent of oxygen used per second
    public Color deathColor;                            // color player's sprite transitions to on death
    public float screenShakeMagnitudeScalar = 0.8f;     // scale by which screen shakes according to damage taken by player
    public float screenShakeRoughness = 4f;             // how rough screen shake is
    public float screenShakeFadeInTime = 0.5f;          // time it takes for screen shake to reach peak magnitude
    public float screenShakeFadeOutTime = 0.5f;         // time it takes for screen shake to end
    public float hurtSoundThreshold = 0.5f;             // amount of oxygen depleted to play a hurt sound effect

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
        myBreathingSource = GetComponent<AudioSource>();
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
        EmptyO2Tank(oxygenDepletionRate * Time.deltaTime, false);

        // scale volume of breathing sound by player's remaining oxygen
        myBreathingSource.volume = Mathf.Clamp01(1 - (currOxygen / maxOxygen));
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
    /// <param name="shakeCamera">flag determining whether to shake camera upon oxygen lost</param>
    void EmptyO2Tank(float amountEmptied, bool shakeCamera)
    {
        // reduce oxygen by amount, killing player if remaining O2 hits 0
        currOxygen = Mathf.Max(0, currOxygen - amountEmptied);
        if (currOxygen <= 0) KillPlayer();

        // shake camera by how much damage player took
        if (shakeCamera)
            CameraShaker.Instance.ShakeOnce((screenShakeMagnitudeScalar * amountEmptied), screenShakeRoughness,
                screenShakeFadeInTime, screenShakeFadeOutTime);

        // if damage exceeds arbitrary threshold, play random hurt sound
        if (amountEmptied >= hurtSoundThreshold)
            AudioManager.Play(myHurtSounds[Random.Range(0, myHurtSounds.Length)], true);

        // update O2 display
        updateO2Event.Invoke(currOxygen);
    }

    /// <summary>
    /// Initiates player death-related events
    /// </summary>
    void KillPlayer()
    {
        // if player hasn't already been soft-disabled
        if (!softDisabled)
        {
            // soft-disable player
            softDisabled = true;
            mySpriteRenderer.color = deathColor;
            myLook.enabled = false;
            myFire.enabled = false;
            myTriggerCollider.enabled = false;

            // play death sound effect
            myBreathingSource.Stop();
            AudioManager.Play(myDeathSound, true);

            // invoking end level event with failure
            endLevelEvent.Invoke(false, 0);
        }

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
