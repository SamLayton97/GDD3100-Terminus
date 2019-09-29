using System.Collections;
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
        EventManager.AddUpdateAmmoUIListener(UpdateAmmo);
    }

    /// <summary>
    /// Updates UI to reflect which weapon user has equipped
    /// </summary>
    /// <param name="newCurrentWeapon">index of weapon equipped by player</param>
    void UpdateCurrentWeapon(int newCurrentWeapon)
    {
        // brighten new current weapon icon
        weaponIcons[newCurrentWeapon].color = equippedColor;

        // darken/deactivate old current weapon icon
        if (currWeaponIndex > 0 && ammoMeters[currWeaponIndex - 1].rectTransform.localScale.x <= 0)
            weaponIcons[currWeaponIndex].color = inactiveColor;
        else
            weaponIcons[currWeaponIndex].color = unequippedColor;
        currWeaponIndex = newCurrentWeapon;
    }

    /// <summary>
    /// Updates UI to reflect amount of ammo in a given weapon
    /// </summary>
    /// <param name="typeToUpdate">which weapon to update ammo of</param>
    /// <param name="remainingAmmo">percentage (0-1) of ammo remaining in weapon</param>
    void UpdateAmmo(WeaponType typeToUpdate, float remainingAmmo)
    {
        // scale corresponding meter by remaining ammo
        ammoMeters[(int)typeToUpdate - 1].rectTransform.localScale =
            new Vector3(remainingAmmo, ammoMeters[(int)typeToUpdate - 1].rectTransform.localScale.y, 
            ammoMeters[(int)typeToUpdate - 1].rectTransform.localScale.z);
    }
}
