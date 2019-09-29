using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi-automatic weapon which fires a single photon
/// each time it registers user input
/// </summary>
public class PhotonThrower : Weapon
{
    /// <summary>
    /// Fires a photon projectile, applying reactionary force to weapon's user
    /// </summary>
    /// <param name="firedLastFrame">whether user of weapon fired on last frame</param>
    public override void RegisterInput(bool firedLastFrame)
    {
        throw new System.NotImplementedException();
    }
}
