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
    /// Adds given method as invoker of this object's pick up weapon event
    /// </summary>
    /// <param name="newListener">added listener to event</param>
    public void AddPickupWeaponInvoker(UnityAction<WeaponType> newListener)
    {
        pickUpEvent.AddListener(newListener);
    }
}
