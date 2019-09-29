﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages weapon list UI to reflect weapons player has,
/// their current weapon, and how much ammo each weapon has.
/// </summary>
public class WeaponSelectUI : MonoBehaviour
{
    // Weapon icon variables
    public Image[] weaponIcons;             // array of icons corresponding to each weapon-type
    public Color equippedColor;             // color of icon when equipped
    public Color unequippedColor;           // color of icon when unequipped
    public Color inactiveColor;             // color of icon when inactive

    // Ammo meter variables
    public Image[] ammoMeters;              // array of meters corresponding to ammo of each weapon

    // support variables
    int currWeaponIndex = 0;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // set colors of all weapons save for first to inactive
        for (int i = 1; i < weaponIcons.Length; i++)
            weaponIcons[i].color = inactiveColor;

        // scale all ammo meters to 0
        for (int i = 0; i < ammoMeters.Length; i++)
            ammoMeters[i].rectTransform.localScale = new Vector3(0, 
                ammoMeters[i].rectTransform.localScale.y, ammoMeters[i].rectTransform.localScale.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to appropriate events
        EventManager.AddSwapWeaponUIListener(UpdateCurrentWeapon);

    }

    /// <summary>
    /// Updates UI to reflect which weapon user has equipped
    /// </summary>
    /// <param name="newCurrentWeapon">index of weapon equipped by player</param>
    void UpdateCurrentWeapon(int newCurrentWeapon)
    {
        Debug.Log(newCurrentWeapon);

        // brighten new current weapon icon and darken old one
        weaponIcons[newCurrentWeapon].color = equippedColor;
        weaponIcons[currWeaponIndex].color = unequippedColor;
        currWeaponIndex = newCurrentWeapon;
    }
}
