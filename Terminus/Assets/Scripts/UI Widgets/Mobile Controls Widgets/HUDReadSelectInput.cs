using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Reads pointer-based input from Weapons HUD, initiating
/// selection of appropriate weapon in player's possession.
/// Used for mobile controls.
/// </summary>
public class HUDReadSelectInput : MonoBehaviour
{
    // event support
    HUDSelectWeaponEvent selectEvent;

    /// <summary>
    /// Used for late initialization
    /// </summary>
    void Start()
    {
        // add self as invoker of HUD select event
        selectEvent = new HUDSelectWeaponEvent();
        EventManager.AddHUDSelectWeaponInvoker(this);
    }

    /// <summary>
    /// Initates weapon selection of particular type from HUD
    /// </summary>
    /// <param name="type">index of selected weapon type within
    /// WeaponTypes enum</param>
    public void SelectWeapon(int typeIndex)
    {
        Debug.Log((WeaponType)typeIndex);
    }

    /// <summary>
    /// Adds given method as listener to HUD select weapon event
    /// </summary>
    /// <param name="newListener">new method listening for event</param>
    public void AddSelectListener(UnityAction<int> newListener)
    {
        selectEvent.AddListener(newListener);
    }

}
