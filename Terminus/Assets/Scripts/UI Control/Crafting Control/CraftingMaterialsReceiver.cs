using System.Collections;
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
    [Range(1, 10)]
    [SerializeField] int maxMaterialsOnDeck = 3;                // max number of items player can push onto crafting menu
    [SerializeField] Transform parentContainer;                 // parent transform of any material on deck in crafting menu
    [SerializeField] GameObject onDeckMaterialTemplate;         // generic UI representation of material added to crafting menu
    [SerializeField] Sprite[] craftingMaterialsIcons;           // list of sprites corresponding to each crafting material
                                                                // NOTE: must be entered in order as they appear in CraftingMaterials enumeration
    [SerializeField] Color[] onDeckIconColors;                  // list of sprite colors corresponding to each crafting material icon
                                                                // NOTE: like above, must be entered in same order as CraftingMaterials enumeration

    // private variables
    List<CraftingMaterials> materialsOnDeck =                   // list of crafting materials "on deck" in crafting menu slots
        new List<CraftingMaterials>();

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // add self as listener to remove material event
        EventManager.AddRemoveMaterialsListener(PushMaterial);
    }

    /// <summary>
    /// Pushes crafting material of given type onto
    /// crafting menu's "On Deck" list
    /// </summary>
    /// <param name="materialToPush">type of material to push</param>
    /// <param name="amount">IGNORED - needed to listen for remove materials event</param>
    void PushMaterial(CraftingMaterials materialToPush, int amount)
    {
        // if materials currently on deck isn't above max
        if (materialsOnDeck.Count < maxMaterialsOnDeck)
        {
            // push crafting material onto deck
            materialsOnDeck.Add(materialToPush);
            CraftingMaterialOnDeck newOnDeck = Instantiate(onDeckMaterialTemplate, parentContainer).GetComponent<CraftingMaterialOnDeck>();

            // modify visual elements of new on deck crafting material
            newOnDeck.Icon = craftingMaterialsIcons[(int)materialToPush];
            newOnDeck.IconColor = onDeckIconColors[(int)materialToPush];
            newOnDeck.MaterialType = materialToPush;
        }
        // otherwise (materials on deck plus new exceeds max)
        else
        {
            // discard material, returning it to player's inventory
            Debug.Log("discarded: " + materialToPush);
            addMaterialsEvent.Invoke(materialToPush, 1);
        }
    }
}
