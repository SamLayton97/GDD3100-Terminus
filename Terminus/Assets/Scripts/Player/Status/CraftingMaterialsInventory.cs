using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enumeration of materials player can craft with
/// </summary>
public enum CraftingMaterials
{
    casing,
    powder,
    biomass
}

/// <summary>
/// Holds amount of crafting materials player has collected
/// (per type), allowing them to craft usable items (e.g.,
/// bio-rifle ammo).
/// </summary>
public class CraftingMaterialsInventory : MonoBehaviour
{
    // private variables
    Dictionary<CraftingMaterials, int> materialsCarried =           // dictionary pairing crafting materials with amount held by player
        new Dictionary<CraftingMaterials, int>();

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize player with empty inventory
        for (int i = 0; i < System.Enum.GetNames(typeof(CraftingMaterials)).Length; i++)
            materialsCarried.Add((CraftingMaterials)i, 0);

        // DEBUGGING: ensure proper initialization
        //for (int i = 0; i < System.Enum.GetNames(typeof(CraftingMaterials)).Length; i++)
        //    Debug.Log((CraftingMaterials)i + " " + materialsCarried[(CraftingMaterials)i]);
    }

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as listener to pick up materials event
        EventManager.AddPickUpMaterialsListener(AddMaterials);
    }

    /// <summary>
    /// Adds materials of given type to player's inventory
    /// </summary>
    /// <param name="materialToAdd">type of material to add</param>
    /// <param name="amount">amount to add</param>
    void AddMaterials(CraftingMaterials materialToAdd, int amount)
    {
        materialsCarried[materialToAdd] += amount;

        // DEBUGGING: display updated amount
        Debug.Log(materialToAdd + " " + materialsCarried[materialToAdd]);
    }
}
