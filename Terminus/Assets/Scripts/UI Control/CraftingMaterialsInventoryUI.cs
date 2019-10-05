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
    [SerializeField] Sprite[] craftingMaterialsIcons;           // list of sprites corresponding to each crafting material
                                                                // NOTE: must be entered in order as they appear in CraftingMaterials enumeration

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as listner to update materials UI event
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
    }
}
