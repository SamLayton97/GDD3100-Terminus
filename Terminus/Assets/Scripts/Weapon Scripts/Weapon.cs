using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all weapons, which spawn some projectile
/// and apply a force to the user in the opposite direction.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    // public variables
    public bool continuousFiring = false;   // whether user may fire weapon for consecutive frames
    public float projectileForce = 5.0f;    // force by which object propels projectile
    public float reactiveForce = 2.5f;      // force by which object propels user in opposite direction
    public int currAmmo = 100;              // current ammunution stored in weapon
    public int maxAmmo = 100;               // max amount of ammunition able to be stored in weapon

    /// <summary>
    /// Registers shot when user fires their weapon.
    /// Note: All weapons must register that fire input from user.
    /// </summary>
    public abstract void RegisterShot();
}
