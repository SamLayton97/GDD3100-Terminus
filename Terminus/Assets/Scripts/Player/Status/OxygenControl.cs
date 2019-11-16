using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

/// <summary>
/// Manages oxygen depletion and re-gain of agent, including instances of player death
/// </summary>
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
    FaceMousePosition myLook;               // player's look-input component (disabled on death)
    PlayerFire myFire;                      // player's combat component (disabled on death)
    CircleCollider2D myTriggerCollider;     // player's trigger collider component (disabled on death)
    bool lowOxygen = false;                 // flag indicating whether player is low on oxygen
    Vector3 regainOrientation
        = new Vector3();

    // depletion configuration variables
    [SerializeField] float oxygenDepletionRate = 1f;                // percent of oxygen used per second

    // audio configuration variables
    [SerializeField] AudioClipNames[] myHurtSounds =              // sound effects played when player is hurt
        {
        AudioClipNames.player_hurt,
        AudioClipNames.player_hurt1,
        AudioClipNames.player_hurt2
        };
    [SerializeField] AudioClipNames myDeathSound =                  // sound effect played when player dies
        AudioClipNames.player_death;
    [SerializeField] float hurtSoundThreshold = 0.5f;               // amount of oxygen depleted to play a hurt sound effect
    
    // screen shake configuration
    [SerializeField] float screenShakeMagnitudeScalar = 0.8f;       // scale by which screen shakes according to damage taken by player
    [SerializeField] float screenShakeRoughness = 4f;               // how rough screen shake is
    [SerializeField] float screenShakeFadeInTime = 0.5f;            // time it takes for screen shake to reach peak magnitude
    [SerializeField] float screenShakeFadeOutTime = 0.5f;           // time it takes for screen shake to end

    // effects configuration variables
    [SerializeField] Transform effectsContainer;                    // transform of child object holding all particle effects
    [SerializeField] GameObject hurtParticleEffect;                 // particle effect played when player loses significant amount of oxygen at once
    [SerializeField] List<GameObject> regainEffects =               // particle effects played (and replayed) when players regains oxygen -- set in editor
        new List<GameObject>();
    [Range(0, 1f)]
    [SerializeField] float effectDelay = 0.5f;                      // delay between playing each regain particle effect
    [SerializeField] float lowOxygenThreshold = 40f;                // arbitrary point where player should be mindful of their oxygen
    [SerializeField] AudioSource myBreathingSource;                 // audio source used to play looping breathing effect

    // event support
    UpdateO2DisplayEvent updateO2Event;                             // event invoked to update player's oxygen on UI

    #region Properties

    /// <summary>
    /// Property with read-access returning amount of O2
    /// left in player's tank.
    /// </summary>
    public float CurrentOxygen
    {
        get { return currOxygen; }
    }

    /// <summary>
    /// Read-access property returning point where player should
    /// be mindful of their oxygen
    /// </summary>
    public float LowOxygenThreshold
    {
        get { return lowOxygenThreshold; }
    }

    #endregion

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components and information
        myLook = GetComponent<FaceMousePosition>();
        myFire = GetComponent<PlayerFire>();
        myTriggerCollider = GetComponent<CircleCollider2D>();
        if (!effectsContainer)
            effectsContainer = transform.GetChild(1);       // assumed to be second child under player
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

        // display audio-visual low oxygen warning if oxygen falls below threshold
        if (!lowOxygen && currOxygen < lowOxygenThreshold)
        {
            lowOxygen = true;
            Notifications.Instance.Display("Monitor Oxygen");
            AudioManager.Play(AudioClipNames.player_oxygenWarning, true);
        }
        // reset low oxygen flag if O2 rises above threshold
        else if (lowOxygen && currOxygen >= lowOxygenThreshold)
            lowOxygen = false;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Refills oxygen tank by set amount, maxing out at tank capacity
    /// </summary>
    /// <param name="amountRefilled">amount of O2 filled</param>
    void RefillO2Tank(float amountRefilled)
    {
        // increase oxygen
        currOxygen = Mathf.Min(maxOxygen, currOxygen + amountRefilled);
        updateO2Event.Invoke(currOxygen);

        // play particle feedback
        StartCoroutine(OpenCannister());
    }

    /// <summary>
    /// Coroutine controlling playing of air burst
    /// particle effects when player collides with 
    /// an oxygen cannister.
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenCannister()
    {
        // play each effect in sequence
        foreach (GameObject effect in regainEffects)
        {
            // play and wait
            effect.SetActive(true);
            yield return new WaitForSeconds(effectDelay);
        }
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

        // if damage was significant enough to shake camera
        if (shakeCamera)
        {
            // scale camera shake by oxygen lost
            CameraShaker.Instance.ShakeOnce((screenShakeMagnitudeScalar * amountEmptied), screenShakeRoughness,
                screenShakeFadeInTime, screenShakeFadeOutTime);

            // play visual feedback
            Instantiate(hurtParticleEffect, effectsContainer.position, Quaternion.identity);
        }

        // if player isn't "dead" and damage exceeds arbitrary threshold, play random hurt sound
        if (!softDisabled && amountEmptied >= hurtSoundThreshold)
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
            myLook.enabled = false;
            myFire.enabled = false;
            myTriggerCollider.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Corpses");

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
