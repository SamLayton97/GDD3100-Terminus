using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration of weapons player can collect.
/// Used for adding weapons to player's collection.
/// </summary>
public enum WeaponType
{
    Pistol,
    Shotgun,
    PhotonThrower,
    BioRifle
}

/// <summary>
/// Registers user's swap weapon input, switching current
/// weapon to next/previous weapon in player's inventory.
/// Also adds and removes weapons from inventory when necessary.
/// </summary>
[RequireComponent(typeof(PlayerFire))]
public class WeaponSelect : MonoBehaviour
{
    // private variables
    PlayerFire playerFire;                                  // player fire component (gets its current weapon property updated)
    AudioClipNames mySwapSound =                            // sound played when player swaps to different weapon
        AudioClipNames.player_swapWeapon;
    Dictionary<WeaponType, GameObject> typeToObject;        // dictionary pairing weapon types with their corresponding weapon objects

    // serialized variables
    [SerializeField] GameObject[] allWeapons;               // serialized array of all weapons objects player could have
                                                            // Note: must be populated in order they appear in enumeration

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        playerFire = GetComponent<PlayerFire>();

        // initialize starting weapon and load type to object dictionary
        playerFire.CurrentWeapon = GetComponentInChildren<Weapon>();
        typeToObject = new Dictionary<WeaponType, GameObject>();
        for (int i = 0; i < allWeapons.Length; i++)
            typeToObject.Add((WeaponType)i, allWeapons[i]);

        // for all additional weapons, spawn them under player but as inactive
        for (int i = 1; i < allWeapons.Length; i++)
            Instantiate(allWeapons[i], transform).SetActive(false);
    }

    /// <summary>
    /// Called once before first frame Update
    /// </summary>
    void Start()
    {
        // add self as listener to relevant events
        EventManager.AddPickUpWeaponListener(AddWeapon);
        EventManager.AddEmptyWeaponListener(HandleEmptyWeapon);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // TEST CODE: add weapon to inventory on input
        if (Input.GetKeyDown(KeyCode.Alpha1))
            AddWeapon(WeaponType.Shotgun);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            AddWeapon(WeaponType.PhotonThrower);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            AddWeapon(WeaponType.BioRifle);

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

        // swap weapon and play sound
        playerFire.CurrentWeapon = transform.GetChild(newWeaponIndex).GetComponent<Weapon>();
        AudioManager.Play(mySwapSound, true);
    }

    /// <summary>
    /// Adds a new weapon to end of player's inventory,
    /// maxing out corresponding weapon's ammo if player
    /// already has it.
    /// </summary>
    /// <param name="newWeapon">type of new weapon to add</param>
    void AddWeapon(WeaponType newWeapon)
    {
        // if corresponding weapon isn't active, activate it
        if (!transform.GetChild((int)newWeapon).gameObject.activeSelf)
            transform.GetChild((int)newWeapon).gameObject.SetActive(true);

        // refill corresponding weapon's ammo
        transform.GetChild((int)newWeapon).GetComponent<Weapon>().RefillAmmo();

        //// iterate over player's current weapons
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    // if new weapon matches existing weapon
        //    if (newWeapon.ToString() + "(Clone)" == transform.GetChild(i).name)
        //    {
        //        // refill weapon's ammo and break from method
        //        transform.GetChild(i).GetComponent<Weapon>().RefillAmmo();
        //        return;
        //    }
        //}

        // instantiate new weapon under player (as they don't have weapon of this type)
        //Instantiate(typeToObject[newWeapon], transform);
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
