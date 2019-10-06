using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Abstract base class which adds allows children to
/// add crafting materials of a given type to player's 
/// inventory.
/// </summary>
public abstract class CraftingMaterialAdder : MonoBehaviour
{
    // event support
    protected AddMaterialsEvent addMaterialsEvent;

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    protected virtual void Start()
    {
        // add self as invoker of add materials event
        addMaterialsEvent = new AddMaterialsEvent();
        EventManager.AddPickUpMaterialsInvoker(this);
    }

    /// <summary>
    /// Adds given listener to add materials event
    /// </summary>
    /// <param name="newListener">new listener for event</param>
    public void AddPickUpMaterialsListener(UnityAction<CraftingMaterials, int> newListener)
    {
        addMaterialsEvent.AddListener(newListener);
    }
}
