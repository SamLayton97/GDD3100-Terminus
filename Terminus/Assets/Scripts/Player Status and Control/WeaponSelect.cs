using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Registers user's swap weapon input, switching current
/// weapon to next/previous weapon in player's inventory.
/// Also adds and removes weapons from inventory when necessary.
/// </summary>
[RequireComponent(typeof(PlayerFire))]
public class WeaponSelect : MonoBehaviour
{
    // private variables
    PlayerFire playerFire;              // player fire component (gets its current weapon property updated)

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        playerFire = GetComponent<PlayerFire>();

        // set starting weapon to first child of player
        playerFire.CurrentWeapon = GetComponentInChildren<Weapon>();
    }

    /// <summary>
    /// Called once before first frame Update
    /// </summary>
    void Start()
    {
        // add self as listener to empty weapon event
        EventManager.AddEmptyWeaponListener(HandleEmptyWeapon);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // swap to next weapon under player on weapon-swap input
        float swapInput = Input.GetAxis("Mouse ScrollWheel");
        if (swapInput != 0)
        {
            // from input, determine index of next current weapon, wrapping if necessary
            int newCurrIndex = playerFire.CurrentWeapon.transform.GetSiblingIndex() + (swapInput > 0 ? -1 : 1);
            if (newCurrIndex >= transform.childCount || newCurrIndex < 0)
                newCurrIndex += transform.childCount * ((newCurrIndex < 0) ? 1 : -1);

            // set current weapon to object residing at index
            SwapWeapon(newCurrIndex);
            
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Swaps player's current weapon to one under it at given index
    /// </summary>
    /// <param name="newWeaponIndex">child index of weapon to swap to</param>
    void SwapWeapon(int newWeaponIndex)
    {
        Debug.Log(newWeaponIndex);

        // swap weapon which registers fire input
        playerFire.CurrentWeapon = transform.GetChild(newWeaponIndex).GetComponent<Weapon>();
    }

    /// <summary>
    /// Handles empty weapon event, swapping current weapon to
    /// previous under player. This method is safe as the first
    /// weapon (pistol) will always have infinite ammo.
    /// </summary>
    void HandleEmptyWeapon()
    {
        SwapWeapon(playerFire.CurrentWeapon.transform.GetSiblingIndex() - 1);
    }

    #endregion

}
