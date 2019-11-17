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
    // configuration variables
    public float sanityLostOnShot = 10f;    // amount of sanity player looses when they fire weapon

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
}
