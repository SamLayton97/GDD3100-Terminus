using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to deduct amount of given
/// crafting material from players inventory.
/// </summary>
public class RemoveMaterialsEvent : UnityEvent<CraftingMaterials, int>
{
}
