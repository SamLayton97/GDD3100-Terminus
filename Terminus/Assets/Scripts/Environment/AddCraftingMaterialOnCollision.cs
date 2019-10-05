using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Adds crafting materials of specific type to player's
/// inventory on collision
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class AddCraftingMaterialOnCollision : MonoBehaviour
{
    // serialized variables
    [SerializeField] CraftingMaterials materialToAdd =  // type of crafting material to give to player on collision
        CraftingMaterials.biomass;
    [Range(1, 10)]
    [SerializeField] int amountToAdd = 1;               // amount of specific material to add to player's inventory

    // event support
    PickUpMaterialsEvent pickUpEvent;                   // event invoked to add crafting materials to player's inventory

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of pickup materials event
        pickUpEvent = new PickUpMaterialsEvent();
        EventManager.AddPickUpMaterialsInvoker(this);
    }

    /// <summary>
    /// Adds given method as listener to pick up materials event
    /// </summary>
    /// <param name="newListener">new listener to event</param>
    public void AddPickUpMaterialsListener(UnityAction<CraftingMaterials, int> newListener)
    {
        pickUpEvent.AddListener(newListener);
    }
}
