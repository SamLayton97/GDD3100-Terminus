using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to add weapons of a
/// given type to player's inventory.
/// </summary>
public class PickUpWeaponEvent : UnityEvent<WeaponType>
{
}
