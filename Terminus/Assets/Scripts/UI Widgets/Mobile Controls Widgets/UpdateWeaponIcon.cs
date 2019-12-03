using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes fire button's icon to reflect current weapon
/// </summary>
public class UpdateWeaponIcon : MonoBehaviour
{
    // icon swap support variables
    [SerializeField] List<Sprite> weaponIcons =         // list of icons corresponding to each weapon type
        new List<Sprite>();                             // NOTE: must be entered as they appear in WeaponTypes enum
    [SerializeField] Image icon;                        // UI image displaying weapon icon

    /// <summary>
    /// Used for late initialization
    /// </summary>
    void Start()
    {
        // add self as listener of Update Current Weapon UI event
        EventManager.AddSwapWeaponUIListener(UpdateIcon);
    }

    /// <summary>
    /// Updates fire button icon to reflect currently
    /// equipped weapon
    /// </summary>
    /// <param name="newWeaponIndex">index of new weapon
    /// in WeaponTypes enum</param>
    void UpdateIcon(int newWeaponIndex)
    {
        icon.sprite = weaponIcons[newWeaponIndex];
    }

}
