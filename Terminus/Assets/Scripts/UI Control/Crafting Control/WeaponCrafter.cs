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
    // UI controlling variables
    [SerializeField] Image craftedItemImage;
    [SerializeField] Text craftedItemNameText;
    [SerializeField] Button craftButton;

}
