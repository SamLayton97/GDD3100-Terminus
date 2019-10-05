using System.Collections;
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
    [SerializeField] Sprite[] craftingMaterialsIcons;           // list of sprites corresponding to each crafting material
                                                                // NOTE: must be entered in order as they appear in CraftingMaterials enumeration

    // private variables
    Dictionary<CraftingMaterials, GameObject> materialHolders =     // dictionary holding UI representations of materials in player's inventory
        new Dictionary<CraftingMaterials, GameObject>();            // (accessible by material type)

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as listener to update materials UI event
        EventManager.AddUpdateMaterialsUIListener(UpdateMaterial);
    }

    /// <summary>
    /// Updates material holders on UI to reflect player's
    /// current inventory, adding or removing holders if necessary
    /// </summary>
    /// <param name="materialToUpdate">type of material to be updated</param>
    /// <param name="newAmount">new amount corresponding to material to update</param>
    void UpdateMaterial(CraftingMaterials materialToUpdate, int newAmount)
    {
        Debug.Log(materialToUpdate + " " + newAmount);

        // if new amount does not remove material from inventory
        if (newAmount > 0)
        {
            // TODO: attempt to update material's amount (assumes material already exists in player's inventory)

        }
        // otherwise (new amount is 0 or less)
        else
        {
            // attempt to remove material type from inventory
            try
            {
                Destroy(materialHolders[materialToUpdate]);
                materialHolders.Remove(materialToUpdate);
            }
            // print warning if material to add does not exist in player's inventory
            catch
            {
                Debug.LogWarning("WARNING: Attempting to remove crafting material in UI that does not exist.");
            }
        }
    }
}
