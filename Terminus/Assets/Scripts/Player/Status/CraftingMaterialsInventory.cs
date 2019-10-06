using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An enumeration of materials player can craft with
/// </summary>
public enum CraftingMaterials
{
    casing,
    powder,
    biomass
}

/// <summary>
/// Holds amount of crafting materials player has collected
/// (per type), allowing them to craft usable items (e.g.,
/// bio-rifle ammo).
/// </summary>
public class CraftingMaterialsInventory : MonoBehaviour
{
    // public variables
    public int materialCap = 9;                                             // max amount player can carry of any type of material

    // private variables
    int[] materialsCarried =                                                    
        new int[System.Enum.GetNames(typeof(CraftingMaterials)).Length];    // array holding how much of each crafting material type player is holding

    // event support
    UpdateMaterialsUIEvent updateUIEvent;                                   // event invoked to update UI to reflect player's materials inventory

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize player with empty inventory
        for (int i = 0; i < materialsCarried.Length; i++)
            materialsCarried[i] = 0;

        // DEBUGGING: ensure proper initialization
        //for (int i = 0; i < materialsCarried.Length; i++)
        //    Debug.Log((CraftingMaterials)i + " " + materialsCarried[i]);
    }

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as invoker of update materials UI event
        updateUIEvent = new UpdateMaterialsUIEvent();
        EventManager.AddUpdateMaterialsUIInvoker(this);

        // add self as listener to relevant events
        EventManager.AddPickUpMaterialsListener(AddMaterials);
        EventManager.AddRemoveMaterialsListener(RemoveMaterials);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Adds materials of given type to player's inventory
    /// </summary>
    /// <param name="materialToAdd">type of material to add</param>
    /// <param name="amount">amount to add</param>
    void AddMaterials(CraftingMaterials materialToAdd, int amount)
    {
        materialsCarried[(int)materialToAdd] = Mathf.Min(materialCap, materialsCarried[(int)materialToAdd] + amount);
        updateUIEvent.Invoke(materialToAdd, materialsCarried[(int)materialToAdd]);
    }

    /// <summary>
    /// Removes materials of given type from player's inventory
    /// </summary>
    /// <param name="materialToRemove">type of material to remove</param>
    /// <param name="amount">amount to remove</param>
    void RemoveMaterials(CraftingMaterials materialToRemove, int amount)
    {
        materialsCarried[(int)materialToRemove] = Mathf.Max(0, materialsCarried[(int)materialToRemove] - amount);
        updateUIEvent.Invoke(materialToRemove, materialsCarried[(int)materialToRemove]);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given method as listener to object's update materials UI event
    /// </summary>
    /// <param name="newListener">new listener to this object's event</param>
    public void AddUpdateMaterialsUIEvent(UnityAction<CraftingMaterials, int> newListener)
    {
        updateUIEvent.AddListener(newListener);
    }

    #endregion

}
