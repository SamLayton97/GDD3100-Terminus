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

    // private variables
    CraftingMaterials myMaterialType = CraftingMaterials.biomass;       // type of crafting material this object corresponds to

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

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as invoker of remove materials event
        removeMaterialsEvent = new RemoveMaterialsEvent();
        EventManager.AddRemoveMaterialsInvoker(this);
    }

    /// <summary>
    /// Adds given method as listener to remove crafting materials event
    /// </summary>
    /// <param name="newListener">new listener to event</param>
    public void AddRemoveMaterialsListener(UnityAction<CraftingMaterials, int> newListener)
    {
        removeMaterialsEvent.AddListener(newListener);
    }

}
