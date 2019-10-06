using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to transfer crafting material from
/// player's inventory to crafting menu
/// </summary>
public class PushMaterialEvent : UnityEvent<CraftingMaterials>
{
}
