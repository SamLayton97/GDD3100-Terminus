using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Adds specific weapon to player's inventory on collision
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class AddWeaponOnCollision : WeaponAdder
{
    // public variables
    public WeaponType myWeaponType =            // type of weapon given to player upon collision
        WeaponType.Pistol;

    // private variables
    AudioClipNames myPickupSound =              // sound played on collision with player
        AudioClipNames.env_pickUpWeapon;

    /// <summary>
    /// Called when incoming collider makes contact with object's collider
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // give player weapon corresponding to pickup's type
        pickUpWeaponEvent.Invoke(myWeaponType);

        // play weapon pickup sound effect
        AudioManager.Play(myPickupSound, true);
    }
}
