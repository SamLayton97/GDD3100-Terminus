using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Abstract base class which adds allows children to
/// add weapons of given type to player's inventory.
/// </summary>
public abstract class WeaponAdder : MonoBehaviour
{
    // event support
    protected PickUpWeaponEvent pickUpWeaponEvent;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected virtual void Start()
    {
        // add self as invoker of pick up weapon event
        pickUpWeaponEvent = new PickUpWeaponEvent();
        EventManager.AddPickUpWeaponInvoker(this);
    }

    /// <summary>
    /// Adds given method as invoker of pick up weapon event
    /// </summary>
    /// <param name="newListener">new listener for event</param>
    public void AddPickUpWeaponListener(UnityAction<WeaponType> newListener)
    {
        pickUpWeaponEvent.AddListener(newListener);
    }
}
