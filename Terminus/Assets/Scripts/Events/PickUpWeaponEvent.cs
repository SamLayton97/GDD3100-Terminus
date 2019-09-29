using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked when player collides with weapon pickup,
/// adding corresponding weapon to their inventory.
/// </summary>
public class PickUpWeaponEvent : UnityEvent<WeaponType>
{
}
