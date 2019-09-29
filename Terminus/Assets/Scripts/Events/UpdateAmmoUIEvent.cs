using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Updates weapon UI to reflect remaining ammo of a given weapon
/// </summary>
public class UpdateAmmoUIEvent : UnityEvent<WeaponType, float>
{
}
