using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Deducts set amount of sanity from player
/// when they collide with trigger
/// </summary>
public class DeductSanityTrigger : SanityDeductor
{
    // configuration variables
    [Range(0, 100)]
    [SerializeField] int sanityLost = 0;        // amount of sanity lost on collision

    /// <summary>
    /// Called when player enters trigger box
    /// </summary>
    /// <param name="other">collision info</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if player's body entered trigger
        if (!other.isTrigger)
        {
            // deduct set amount of sanity
            deductSanityEvent.Invoke(sanityLost);
        }
    }
}
