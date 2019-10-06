using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Determines whether player can craft a weapon
/// from materials on deck, displaying weapon if
/// that's the case, and adding weapon to player's
/// inventory on user input.
/// </summary>
[RequireComponent(typeof(CraftingMaterialsReceiver))]
public class WeaponCrafter : WeaponAdder
{
    // serialized UI controlling variables
    [SerializeField] Image craftedItemImage;
    [SerializeField] Text craftedItemNameText;
    [SerializeField] Button craftButton;

    // private variables
    CraftingMaterialsReceiver myReceiver;           // component controlling receiving & removal of materials from crafting menu

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant components
        myReceiver = GetComponent<CraftingMaterialsReceiver>();

        // initialize crafted item to empty
        craftedItemImage.color = new Color(craftedItemImage.color.r, craftedItemImage.color.g, craftedItemImage.color.b, 0);
        craftedItemNameText.text = "";
        craftButton.interactable = false;
    }

    /// <summary>
    /// Determines whether player is able to craft anything given entered materials
    /// </summary>
    /// <param name="materialsOnDeck">array of materials in crafting menu</param>
    public void DetermineItemFromMaterials(CraftingMaterials[] materialsOnDeck)
    {
        // search registry for crafted weapon
        WeaponType craftedType = CraftableItemsRegistry.GetCraftableItem(materialsOnDeck);

        // if search yielded anything (i.e., not pistol as that is never craftable)
        if (craftedType != WeaponType.Pistol)
        {
            // play higher pitched push sound
            AudioManager.Play(AudioClipNames.UI_pushLastMaterial, true);

            Debug.Log(craftedType);
        }
        // otherwise (user didn't enter good combination)
        else
        {
            // play standard push items sound
            AudioManager.Play(AudioClipNames.UI_pushMaterial, true);
            Debug.Log("turkey");
        }
    }
}
