using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Procides access to relevant aspects of a crafting
/// material displayed on the player's UI inventory. Also
/// responds to user input, adding item to crafting panel.
/// </summary>
public class CraftingMaterialHolder : MonoBehaviour
{
    // serialized variables
    [SerializeField] Image materialIcon;
    [SerializeField] Text materialAmount;
    [SerializeField] Text materialName;
    [SerializeField] Image borderImage;
    [SerializeField] Color unhighlightedBorderColor;        // color of holder's border when not moused over
    [SerializeField] Color unhighlightedTextColor;          // color of holder's text when not moused over
    [SerializeField] AudioClipNames pushSound =             // sound played when user pushes material to crafting popup
        AudioClipNames.UI_pushMaterial;

    // private variables
    CraftingMaterials myMaterialType = CraftingMaterials.biomass;       // type of crafting material this object corresponds to
    Color highlightedBorderColor;                                       // color of holder's border when moused over
    Color highlightedTextColor;                                         // color of holder's text when moused over

    // event support
    RemoveMaterialsEvent removeMaterialsEvent;

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
    /// Property with write access to remaining
    /// amount of crafting material
    /// </summary>
    public int Amount
    {
        set { materialAmount.text = value.ToString(); }
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
        highlightedTextColor = materialAmount.color;

        // initialize border and text to use unhighlighted colors
        DarkenMaterialHolder();
    }

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as invoker of remove materials event
        removeMaterialsEvent = new RemoveMaterialsEvent();
        EventManager.AddRemoveMaterialsInvoker(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PushToCraftingMenu();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// When user mouses over material holder, brighten
    /// its border and text.
    /// </summary>
    public void HighlightMaterialHolder()
    {
        borderImage.color = highlightedBorderColor;
        materialAmount.color = highlightedTextColor;
        materialName.color = highlightedTextColor;
    }

    /// <summary>
    /// When user's mouse leaves material holder, darken
    /// its border and text
    /// </summary>
    public void DarkenMaterialHolder()
    {
        borderImage.color = unhighlightedBorderColor;
        materialAmount.color = unhighlightedTextColor;
        materialName.color = unhighlightedTextColor;
    }

    /// <summary>
    /// On user input (click by default), push this item to crafting menu,
    /// removing it from player's inventory
    /// </summary>
    public void PushToCraftingMenu()
    {
        // TODO: push to crafting menu
        

        // deduct 1 from remaining materials of this type
        removeMaterialsEvent.Invoke(myMaterialType, 1);
    }

    /// <summary>
    /// Adds given method as listener to remove crafting materials event
    /// </summary>
    /// <param name="newListener">new listener to event</param>
    public void AddRemoveMaterialsListener(UnityAction<CraftingMaterials, int> newListener)
    {
        removeMaterialsEvent.AddListener(newListener);
    }

    #endregion

}
