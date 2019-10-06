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
    static Dictionary<CraftingMaterials[], WeaponType> readInMaterialsToWeapons =       // designer-entered combination of crafting mateials and their craftable weapon
        new Dictionary<CraftingMaterials[], WeaponType>();
    static Dictionary<CraftingMaterials[], WeaponType> materialsToWeapons =             // dictionary holding all possible combinations of materials to weapons
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
        readInMaterialsToWeapons.Add( new CraftingMaterials[] { CraftingMaterials.biomass, CraftingMaterials.casing, CraftingMaterials.powder}, 
            WeaponType.BioRifle);

        // for each combination and yield
        foreach (KeyValuePair<CraftingMaterials[], WeaponType> combination in readInMaterialsToWeapons)
        {
            // add all possible permutations into materials to weapons registry
            //materialsToWeapons.Add(combination.Key, combination.Value);
            
        }
    }

    /// <summary>
    /// Accesses materials-to-weapons registry, returning craftable
    /// weapon given that entered combination exists.
    /// </summary>
    /// <param name="materialsCombination">combination of crafting materials to check for</param>
    /// <returns>craftable weapon, returning Pistol (non-craftable) if combination yields nothing</returns>
    public static WeaponType GetCraftableItem(CraftingMaterials[] materialsCombination)
    {
        // return craftable weapon type if it exists
        return (materialsToWeapons.ContainsKey(materialsCombination) ? materialsToWeapons[materialsCombination] : WeaponType.Pistol);
    }

    /// <summary>
    /// Adds all possible permutations of a given
    /// crafting material combination into registry.
    /// </summary>
    /// <param name="combination">combination of materials to permute</param>
    /// <param name="startingIndex">starting index of materials array</param>
    /// <param name="length">length of materials array</param>
    /// <returns></returns>
    static void Permute(CraftingMaterials[] combination, WeaponType craftableItem, int startingIndex, int length)
    {

    }
}
