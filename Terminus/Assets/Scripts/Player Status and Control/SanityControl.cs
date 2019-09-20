using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages sanity depletion and re-gain of agent, 
/// including (TODO) hallucinations and player death
/// </summary>
[RequireComponent(typeof(OxygenControl))]
[RequireComponent(typeof(CircleCollider2D))]
public class SanityControl : MonoBehaviour
{
    // public variables
    public float sanityReductionRate = 0.5f;    // amount of sanity lost per second when player undergoes stressful situation
    public float lowOxygenThreshold = 40f;      // inclusive threshold on which player starts losing sanity due to low oxygen

    // private variables
    int maxSanity = 100;                        // max sanity player can have
    float currSanity = 0;                       // remaining percent of player's sanity
    OxygenControl myOxygenControl;              // reference to player's oxygen control (sanity depletes when below O2 threshold)
    CircleCollider2D myProximityTrigger;        // reference to player's circle collider (used to deduct sanity when enemies/corruption are nearby)

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

    /// <summary>
    /// "Maddens" player by set amount, stopping at 0
    /// (i.e., totally insane)
    /// </summary>
    /// <param name="sanityLost">amount of sanity lost</param>
    void DeductSanity(float sanityLost)
    {
        currSanity = Mathf.Max(0, currSanity - sanityLost);
    }
}
