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
        readInMaterialsToWeapons.Add(new CraftingMaterials[] { CraftingMaterials.biomass, CraftingMaterials.casing, CraftingMaterials.powder },
            WeaponType.BioRifle);
        readInMaterialsToWeapons.Add(new CraftingMaterials[] { CraftingMaterials.casing, CraftingMaterials.biomass, CraftingMaterials.powder },
            WeaponType.BioRifle);

        // for each combination and yield, add all possible permutations into materials to weapons registry
        foreach (KeyValuePair<CraftingMaterials[], WeaponType> combination in readInMaterialsToWeapons)
            Permute(combination.Key, combination.Value, 0, combination.Key.Length - 1);
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
    /// NOTE: Code taken from:
    /// https://www.geeksforgeeks.org/c-program-to-print-all-permutations-of-a-given-string-2/
    /// </summary>
    /// <param name="combination">combination of materials to permute</param>
    /// <param name="startingIndex">index of first element in combination</param>
    /// <param name="endingIndex">index of last element in combination</param>
    static void Permute(CraftingMaterials[] combination, WeaponType craftableItem, int startingIndex, int endingIndex)
    {
        // if starting index matches end (fully permuted)
        if (startingIndex == endingIndex)
        {
            // add deep copy of combination to registry
            CraftingMaterials[] newCombo = new CraftingMaterials[combination.Length];
            for (int i = 0; i < combination.Length; i++)
            {
                newCombo[i] = combination[i];
            }
            materialsToWeapons.Add(newCombo, craftableItem);
        }
        // otherwise (still needs to be permuted)
        else
        {
            // swap elements in current combination and continue permuting
            for (int i = startingIndex; i <= endingIndex; i++)
            {
                combination = SwapElements(combination, startingIndex, i);
                Permute(combination, craftableItem, startingIndex + 1, endingIndex);
                combination = SwapElements(combination, startingIndex, i);
            }
        }
    }

    /// <summary>
    /// Swaps two elements in combination, returning new combination.
    /// Code based on:
    /// https://www.geeksforgeeks.org/c-program-to-print-all-permutations-of-a-given-string-2/
    /// </summary>
    /// <param name="combination">combination to swap elements within</param>
    /// <param name="a">element to swap with b</param>
    /// <param name="b">element to swap with a</param>
    /// <returns>combination with swapped elements</returns>
    static CraftingMaterials[] SwapElements(CraftingMaterials[] combination, int a, int b)
    {
        CraftingMaterials temp = combination[a];
        combination[a] = combination[b];
        combination[b] = temp;
        return combination;
    }
}
