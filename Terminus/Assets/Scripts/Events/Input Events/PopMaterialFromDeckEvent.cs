using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to remove on deck material from 
/// crafting menu's internal list of items
/// </summary>
public class PopMaterialFromDeckEvent : UnityEvent<CraftingMaterials>
{
}
