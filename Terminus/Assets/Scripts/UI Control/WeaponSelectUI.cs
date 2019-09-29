using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages weapon list UI to reflect weapons player has,
/// their current weapon, and how much ammo each weapon has.
/// </summary>
public class WeaponSelectUI : MonoBehaviour
{
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
