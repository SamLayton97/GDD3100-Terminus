using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages crafting menu, allowing players to push
/// materials onto the menu, pop them off, and create
/// items given the right materials.
/// </summary>
public class CraftingMenu : WeaponAdder
{
    // serialized variables
    [SerializeField] GameObject onDeckMaterialTemplate;         // generic UI representation of material added to crafting menu
    [SerializeField] Sprite[] craftingMaterialsIcons;           // list of sprites corresponding to each crafting material
                                                                // NOTE: must be entered in order as they appear in CraftingMaterials enumeration
    [SerializeField] Color[] iconColors;                        // list of sprite colors corresponding to each crafting material icon
                                                                // NOTE: like above, must be entered in same order as CraftingMaterials enumeration

    // private variables


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }
}
