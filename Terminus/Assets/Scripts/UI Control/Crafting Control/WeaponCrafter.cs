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

    // serialized variables
    [SerializeField] Sprite[] craftedWeaponIcons;           // icons representing each craftable weapon (NOTE: must enter in order they appear in WeaponTypes enum)

    // private variables
    WeaponType currCraftableWeapon = WeaponType.Pistol;     // weapon type user can craft when craft button is interactable
    CraftingMaterialsReceiver myReceiver;                   // component controlling receiving & removal of materials from crafting menu

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant components
        myReceiver = GetComponent<CraftingMaterialsReceiver>();

        // initialize craftable item slot to empty
        EmptyCraftableItemSlot();
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
            // set current craftable weapon to one retrieved from registry
            currCraftableWeapon = craftedType;

            // show appropriate craftable weapon on menu
            craftedItemImage.sprite = craftedWeaponIcons[(int)craftedType];
            craftedItemImage.color = new Color(craftedItemImage.color.r, craftedItemImage.color.g, craftedItemImage.color.b, 1);
            craftedItemNameText.text = craftedType.ToString();

            // set craft button to interactable
            craftButton.interactable = true;

            // play higher pitched push sound
            AudioManager.Play(AudioClipNames.UI_pushLastMaterial, true);
        }
        // otherwise (user didn't enter good combination)
        else
        {
            // play standard push items sound
            AudioManager.Play(AudioClipNames.UI_pushMaterial, true);
        }
    }

    /// <summary>
    /// Called when user clicks craft button
    /// </summary>
    public void CraftWeapon()
    {
        // add craftable weapon to player's inventory and play sound
        pickUpWeaponEvent.Invoke(currCraftableWeapon);
        AudioManager.Play(AudioClipNames.env_pickUpWeapon, true);
        
        // TODO: clear materials on deck

    }

    /// <summary>
    /// Empties craftable item slot of any content,
    /// preventing user from crafting something.
    /// </summary>
    public void EmptyCraftableItemSlot()
    {
        // set current item to safe default
        currCraftableWeapon = WeaponType.Pistol;

        // hide icon and text
        craftedItemImage.color = new Color(craftedItemImage.color.r, craftedItemImage.color.g, craftedItemImage.color.b, 0);
        craftedItemNameText.text = "";

        // set craft button to uninteractable
        craftButton.interactable = false;
    }
}
