using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple semi-automatic pistol used by player
/// </summary>
public class Pistol : Weapon
{
    /// <summary>
    /// Fires a pistol shot, applying reactionary force to agent who shot weapon
    /// </summary>
    /// <param name="firedLastFrame">whether user of weapon fired on last frame</param>
    public override void RegisterInput(bool firedLastFrame)
    {
        // if player didn't fire last frame, register a shot
        if (!firedLastFrame)
            Debug.Log("Pistol shot registered");
    }
}
