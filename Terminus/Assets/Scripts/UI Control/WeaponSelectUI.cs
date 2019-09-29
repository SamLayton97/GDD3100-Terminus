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

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // set colors of all weapons save for first to inactive
        for (int i = 1; i < weaponIcons.Length; i++)
            weaponIcons[i].color = inactiveColor;
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
    /// <param name="currWeaponIndex">index of weapon equipped by player</param>
    void UpdateCurrentWeapon(int currWeaponIndex)
    {
        Debug.Log(currWeaponIndex);
    }
}
