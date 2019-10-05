using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public CraftingMaterials MaterialName
    {
        set { materialName.text = value.ToString(); }
    }

    #endregion

}
