using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Updates crafting materials UI to reflect amount and type
/// carried by player
/// </summary>
public class UpdateMaterialsUIEvent : UnityEvent<CraftingMaterials, int>
{
}
