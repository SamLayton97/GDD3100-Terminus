﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages crafting material inventory UI to reflect materials
/// player has and how much they have of each of them.
/// </summary>
public class CraftingMaterialsInventoryUI : MonoBehaviour
{
    // serialized variables
    [SerializeField] GameObject defaultMaterialHolder;          // generic UI representation of material in player's inventory
    [SerializeField] Transform parentContainer;                 // parent transform of any material holders in player's inventory
    [SerializeField] Sprite[] craftingMaterialsIcons;           // list of sprites corresponding to each crafting material
                                                                // NOTE: must be entered in order as they appear in CraftingMaterials enumeration

    // private variables
    Dictionary<CraftingMaterials, CraftingMaterialHolder> materialHolders =         // dictionary holding UI representations of materials in player's inventory
        new Dictionary<CraftingMaterials, CraftingMaterialHolder>();                // (accessible by material type)

    #region Unity Methods

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as listener to update materials UI event
        EventManager.AddUpdateMaterialsUIListener(UpdateMaterial);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates material holders on UI to reflect player's
    /// current inventory, adding or removing holders if necessary
    /// </summary>
    /// <param name="materialToUpdate">type of material to be updated</param>
    /// <param name="newAmount">new amount corresponding to material to update</param>
    void UpdateMaterial(CraftingMaterials materialToUpdate, int newAmount)
    {
        // if new amount does not remove material from inventory
        if (newAmount > 0)
        {
            // attempt to update material's amount (assumes material already exists in player's inventory)
            try
            {
                materialHolders[materialToUpdate].Amount = newAmount;
            }
            // add new material holder to list (accessing material on UI by type returned nothing)
            catch
            {
                // add new material to inventory
                materialHolders.Add(materialToUpdate, 
                    Instantiate(defaultMaterialHolder, parentContainer).GetComponent<CraftingMaterialHolder>());

                // set icon, color, amount, and name of material
                materialHolders[materialToUpdate].Icon = craftingMaterialsIcons[(int)materialToUpdate];
                materialHolders[materialToUpdate].Amount = newAmount;
                materialHolders[materialToUpdate].MaterialType = materialToUpdate;
            }
        }
        // otherwise (new amount is 0 or less)
        else
        {
            // attempt to remove material type from inventory
            try
            {
                Destroy(materialHolders[materialToUpdate].gameObject);
                materialHolders.Remove(materialToUpdate);
            }
            // print warning if material to add does not exist in player's inventory
            catch
            {
                Debug.LogWarning("WARNING: Attempting to remove crafting material in UI that does not exist.");
            }
        }
    }

    #endregion

}
