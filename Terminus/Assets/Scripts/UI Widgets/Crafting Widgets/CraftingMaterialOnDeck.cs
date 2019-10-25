using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [SerializeField] Image highlightImage;
    [SerializeField] Color unhighlightedBorderColor;        // color of holder's border when not moused over
    [SerializeField] Color unhighlightedTextColor;          // color of holder's text when not moused over
    [SerializeField]
    AudioClipNames popSound =                               // sound played when user removes material from deck
        AudioClipNames.UI_popMaterial;

    // private variables
    CraftingMaterials myMaterialType = CraftingMaterials.biomass;       // type of crafting material this object corresponds to
    Color highlightedBorderColor;                                       // color of holder's border when moused over
    Color highlightedTextColor;                                         // color of holder's text when moused over

    // event support
    PopMaterialFromDeckEvent popMaterialEvent;              // event used to internally remove material from crafting menu

    #region Properties

    /// <summary>
    /// Property with write access to sprite
    /// representing crafting material
    /// </summary>
    public Sprite Icon
    {
        set { materialIcon.sprite = value; }
    }

    /// <summary>
    /// Property with write access to color
    /// of sprite representing material type
    /// </summary>
    public Color IconColor
    {
        set { materialIcon.color = value; }
    }

    /// <summary>
    /// Property with write access to name
    /// of crafting material this object represents
    /// </summary>
    public CraftingMaterials MaterialType
    {
        set
        {
            // update type and text displayed to user
            myMaterialType = value;
            materialName.text = value.ToString();
        }
    }

    #endregion

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve original (highlighted) colors of holder's texts and border
        highlightedBorderColor = borderImage.color;
        highlightedTextColor = materialName.color;

        // initialize border and text to use unhighlighted colors
        DarkenMaterialHolder();
    }

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // add self as invoker of pop material event
        popMaterialEvent = new PopMaterialFromDeckEvent();
        EventManager.AddPopMaterialInvoker(this);
    }

    #endregion

    #region Public Methods 

    /// <summary>
    /// When user mouses over on deck material, brighten
    /// its border and text.
    /// </summary>
    public void HighlightMaterialHolder()
    {
        borderImage.color = highlightedBorderColor;
        materialName.color = highlightedTextColor;
        highlightImage.enabled = true;

        // play highlight sound effect
        AudioManager.Play(AudioClipNames.UI_buttonHighlight, true);
    }

    /// <summary>
    /// When user's mouse leaves on deck material, darken
    /// its border and text
    /// </summary>
    public void DarkenMaterialHolder()
    {
        borderImage.color = unhighlightedBorderColor;
        materialName.color = unhighlightedTextColor;
        highlightImage.enabled = false;
    }

    /// <summary>
    /// On user input (mouse 0 by default), remove this material
    /// from crafting menu, returning it to player's inventory
    /// </summary>
    public void PopFromCraftingMenu()
    {
        // return materials and play pop sound effect
        addMaterialsEvent.Invoke(myMaterialType, 1);
        AudioManager.Play(popSound, true);

        // pop self from crafting deck's list and destroy self
        popMaterialEvent.Invoke(myMaterialType);
        Destroy(gameObject);
    }

    /// <summary>
    /// Adds given method as listener to pop material from deck event
    /// </summary>
    /// <param name="newListener">new listener to event</param>
    public void AddPopMaterialListener(UnityAction<CraftingMaterials> newListener)
    {
        popMaterialEvent.AddListener(newListener);
    }

    #endregion

}
