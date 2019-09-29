using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi-automatic weapon which fires a single
/// "corrupted"/poisoned bullet each time it registers
/// fire input
/// </summary>
public class BioRifle : Weapon
{
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
            Debug.Log("bioshot");
        }
    }
}
