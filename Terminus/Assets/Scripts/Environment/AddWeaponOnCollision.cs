using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Adds specific weapon to player's inventory on collision
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class AddWeaponOnCollision : MonoBehaviour
{
    // public variables
    public WeaponType myWeaponType =            // type of weapon given to player upon collision
        WeaponType.Pistol;

    // private variables
    AudioClipNames myPickupSound =              // sound played on collision with player
        AudioClipNames.env_pickUpWeapon;

    // event support
    PickUpWeaponEvent pickUpEvent;              // event invoked on collision with player

    /// <summary>
    /// Called before first frame of Update()
    /// </summary>
    void Start()
    {
        // add self as invoker of pick up weapon event
        pickUpEvent = new PickUpWeaponEvent();
        EventManager.AddPickUpWeaponInvoker(this);
    }

    /// <summary>
    /// Called when incoming collider makes contact with object's collider
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // give player weapon corresponding to pickup's type
        pickUpEvent.Invoke(myWeaponType);

        // play weapon pickup sound effect
        AudioManager.Play(myPickupSound, true);
    }

    /// <summary>
    /// Adds given method as invoker of this object's pick up weapon event
    /// </summary>
    /// <param name="newListener">added listener to event</param>
    public void AddPickupWeaponInvoker(UnityAction<WeaponType> newListener)
    {
        pickUpEvent.AddListener(newListener);
    }
}
