using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Semi-automatic weapon which fires a single
/// "corrupted"/poisoned bullet each time it registers
/// fire input
/// </summary>
public class BioRifle : Weapon
{
    // public variables
    public float sanityLostOnShot = 10f;    // amount of sanity player looses when they fire weapon

    // event support
    DeductSanityOnFire deductSanityEvent;

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    void Start()
    {
        // add self as invoker of deduct sanity on fire event
        deductSanityEvent = new DeductSanityOnFire();
        EventManager.AddDeductSanityOnFireInvoker(this);
    }

    /// <summary>
    /// Fires a bio-projectile, applying reactionary force to user
    /// and reducing their sanity
    /// </summary>
    /// <param name="firedLastFrame">whether user of weapon fired on last frame</param>
    public override void RegisterInput(bool firedLastFrame)
    {
        // if player didn't fire last frame, register fire input
        if (!firedLastFrame)
        {
            // reduce player's sanity by set amount
            deductSanityEvent.Invoke(sanityLostOnShot);

            // perform basic fire input behavior
            base.RegisterInput(firedLastFrame);
        }
    }

    /// <summary>
    /// Adds given method as listener to deduct sanity on fire event
    /// </summary>
    /// <param name="newListener">new listener of event</param>
    public void AddDeductSanityInvoker(UnityAction<float> newListener)
    {
        deductSanityEvent.AddListener(newListener);
    }
}
