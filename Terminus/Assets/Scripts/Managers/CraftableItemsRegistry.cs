using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all combinations of crafting materials and
/// the items (currently just weapons) they create.
/// </summary>
public static class CraftableItemsRegistry
{
    // private variables
    static Dictionary<CraftingMaterials[], WeaponType> materialsToWeapons =
        new Dictionary<CraftingMaterials[], WeaponType>();

    /// <summary>
    /// Initializes registry by pairing set of crafting materials with
    /// craftable weapons.
    /// TODO: Initialize by reading from file rather than hard-coding
    /// combinations in this method.
    /// </summary>
    public static void Initialize()
    {
        // pair material combinations with craftable weapons
        materialsToWeapons.Add( new CraftingMaterials[] { CraftingMaterials.biomass, CraftingMaterials.casing, CraftingMaterials.powder}, 
            WeaponType.BioRifle);
    }
}
