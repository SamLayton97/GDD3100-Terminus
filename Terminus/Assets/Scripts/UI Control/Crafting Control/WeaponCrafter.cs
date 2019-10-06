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
public class WeaponCrafter : WeaponAdder
{
    // serialized UI controlling variables
    [SerializeField] Image craftedItemImage;
    [SerializeField] Text craftedItemNameText;
    [SerializeField] Button craftButton;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize crafted item to empty
        craftedItemImage.color = new Color(craftedItemImage.color.r, craftedItemImage.color.g, craftedItemImage.color.b, 0);
        craftedItemNameText.text = "";
        craftButton.interactable = false;
    }
}
