using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked when player collides with crafting material pickup,
/// adding amount of corresponding material type to their inventory.
/// </summary>
public class PickUpMaterialsEvent : UnityEvent<CraftingMaterials, int>
{
}
