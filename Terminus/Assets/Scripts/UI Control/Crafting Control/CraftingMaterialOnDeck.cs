using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides access to relevant aspects of a crafting
/// material displayed on the crafting menu. Also
/// responds to user input, popping them from deck.
/// </summary>
public class CraftingMaterialOnDeck : CraftingMaterialAdder
{
    // serialized variables
    [SerializeField] Image materialIcon;
    [SerializeField] Text materialName;
    [SerializeField] Image borderImage;
    [SerializeField] Color unhighlightedBorderColor;        // color of holder's border when not moused over
    [SerializeField] Color unhighlightedTextColor;          // color of holder's text when not moused over
    [SerializeField]
    AudioClipNames pushSound =                              // sound played when user pushes material to crafting popup
        AudioClipNames.UI_pushMaterial;

    // private variables
    CraftingMaterials myMaterialType = CraftingMaterials.biomass;       // type of crafting material this object corresponds to
    Color highlightedBorderColor;                                       // color of holder's border when moused over
    Color highlightedTextColor;                                         // color of holder's text when moused over

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve original (highlighted) colors of holder's texts and border
        highlightedBorderColor = borderImage.color;
        highlightedTextColor = materialName.color;

        // TODO: initialize border and text to use unhighlighted colors
        //DarkenMaterialHolder();
    }
}
