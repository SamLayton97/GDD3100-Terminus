using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Receives crafting materials from inventory, allowing
/// user to remove items from "on-deck" and handling when
/// user attempts to add too many materials.
/// </summary>
public class CraftingMaterialsReceiver : CraftingMaterialAdder
{
    // serialized variables
    [SerializeField] GameObject onDeckMaterialTemplate;         // generic UI representation of material added to crafting menu
    [SerializeField] Sprite[] craftingMaterialsIcons;           // list of sprites corresponding to each crafting material
                                                                // NOTE: must be entered in order as they appear in CraftingMaterials enumeration
    [SerializeField] Color[] iconColors;                        // list of sprite colors corresponding to each crafting material icon
                                                                // NOTE: like above, must be entered in same order as CraftingMaterials enumeration

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // TODO: add self as 
    }
}
