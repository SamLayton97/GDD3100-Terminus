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

            Debug.Log(newCurrIndex);
            // set current weapon to object residing at index
            playerFire.CurrentWeapon = transform.GetChild(newCurrIndex).GetComponent<Weapon>();
        }
    }
}
