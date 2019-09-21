using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages sanity depletion and re-gain of agent, 
/// including (FUTURE ITERATIONS) hallucinations and player death
/// </summary>
[RequireComponent(typeof(OxygenControl))]
[RequireComponent(typeof(CircleCollider2D))]
public class SanityControl : MonoBehaviour
{
    // public variables
    public float sanityReductionRate = 0.5f;        // amount of sanity lost per second when player undergoes stressful situation
    public float sanityReplinishmentRate = 0.1f;    // amount of sanity gained per second when player avoids stressful situation
    public float lowOxygenThreshold = 40f;          // inclusive threshold on which player starts losing sanity due to low oxygen

    // private variables
    int maxSanity = 100;                            // max sanity player can have
    float currSanity = 0;                           // remaining percent of player's sanity
    float sanityLastFrame = 0;                      // variable storing player's sanity on the previous frame (used to control sanity replinishment)
    OxygenControl myOxygenControl;                  // reference to player's oxygen control (sanity depletes when below O2 threshold)
    CircleCollider2D myProximityTrigger;            // reference to player's circle collider (used to deduct sanity when enemies/corruption are nearby)

    // event support
    UpdateSanityDisplayEvent updateDisplayEvent;    // event invoked to update UI corresponding to player's sanity

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myOxygenControl = GetComponent<OxygenControl>();
    }

    /// <summary>
    /// Called once before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as invoker of relevant events
        updateDisplayEvent = new UpdateSanityDisplayEvent();
        EventManager.AddUpdateSanityInvoker(this);

        // initialize player with full sanity
        currSanity = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        // TEMP: simply log current sanity to console
        Debug.Log("Sanity: " + currSanity);

        // reduce sanity by rate if player lacks oxygen
        DeductSanity((myOxygenControl.CurrentOxygen <= lowOxygenThreshold) ? (sanityReductionRate * Time.deltaTime) : 0);

        // if player hasn't undergone things causing stress, replinish sanity by rate
        if (currSanity >= sanityLastFrame)
            ReplinishSanity(sanityReplinishmentRate * Time.deltaTime);
    }

    /// <summary>
    /// Called once per frame after Update() finishes
    /// </summary>
    void LateUpdate()
    {
        // store sanity of previous frame
        sanityLastFrame = currSanity;
    }

    /// <summary>
    /// Called every frame something stays within player's circle collider trigger
    /// </summary>
    void OnTriggerStay2D(Collider2D collision)
    {
        // if other object resides on alien layer, deduct player sanity
        if (collision.gameObject.layer == LayerMask.NameToLayer("Alien"))
            DeductSanity(sanityReductionRate * Time.deltaTime);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// "Maddens" player by set amount, stopping at 0
    /// (i.e., totally insane)
    /// </summary>
    /// <param name="sanityLost">amount of sanity lost</param>
    void DeductSanity(float sanityLost)
    {
        currSanity = Mathf.Max(0, currSanity - sanityLost);
        updateDisplayEvent.Invoke(currSanity);
    }

    /// <summary>
    /// Increases player's sanity by set amount, stopping at
    /// max sanity (i.e, perfect mental health)
    /// </summary>
    /// <param name="sanityGained">amount of sanity gained</param>
    void ReplinishSanity(float sanityGained)
    {
        currSanity = Mathf.Min(100, currSanity + sanityGained);
        updateDisplayEvent.Invoke(currSanity);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds a given method as a listener for the update sanity display event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddUpdateSanityListener(UnityAction<float> newListener)
    {
        updateDisplayEvent.AddListener(newListener);
    }

    #endregion

}
