using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event used to select weapon in player's possession from
/// weapon display HUD. Used for mobile controls.
/// </summary>
public class HUDSelectWeaponEvent : UnityEvent<int>
{
}
