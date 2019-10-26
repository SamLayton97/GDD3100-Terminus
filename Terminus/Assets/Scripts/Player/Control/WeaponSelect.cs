using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // event support
    SwapWeaponUIEvent updateCurrentWeapon;                  // event invoked to update player's current weapon on HUD

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

        // for all limited-ammo weapons, spawn them under player but as inactive
        for (int i = 1; i < allWeapons.Length; i++)
            Instantiate(allWeapons[i], transform).SetActive(false);
    }

    /// <summary>
    /// Called once before first frame Update
    /// </summary>
    void Start()
    {
        // add self as invoker of relevant events
        updateCurrentWeapon = new SwapWeaponUIEvent();
        EventManager.AddSwapWeaponUIInvoker(this);

        // add self as listener to relevant events
        EventManager.AddPickUpWeaponListener(AddWeapon);
        EventManager.AddEmptyWeaponListener(HandleEmptyWeapon);

        // initialize starting weapon
        SelectWeapon((int)WeaponType.Pistol);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // CHEAT CODE: add weapon to inventory on input
        if (Input.GetKeyDown(KeyCode.Alpha1))
            AddWeapon(WeaponType.Shotgun);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            AddWeapon(WeaponType.PhotonThrower);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            AddWeapon(WeaponType.BioRifle);

        // read from different inputs depending on control scheme
        if (ControlSchemeManager.CurrentScheme == ControlSchemes.Specialist || ControlSchemeManager.CurrentScheme == ControlSchemes.LeftySpecialist)
        {
            // directly select weapon on input
            if (CustomInputManager.GetKeyDown("SelectWeapon1"))
                SelectWeapon(0);
            else if (CustomInputManager.GetKeyDown("SelectWeapon2"))
                SelectWeapon(1);
            else if (CustomInputManager.GetKeyDown("SelectWeapon3"))
                SelectWeapon(2);
            else if (CustomInputManager.GetKeyDown("SelectWeapon4"))
                SelectWeapon(3);
        }
        else
        {
            // swap to next weapon under player on weapon-swap input
            float swapInput = Input.GetAxis("Mouse ScrollWheel");
            if (swapInput != 0)
            {
                // from input swap to next active weapon
                SwapWeapon(swapInput < 0);
            }
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Swaps player's current weapon to neighbor according to user input
    /// </summary>
    /// <param name="swapNext">flag determining direction to swap in</param>
    void SwapWeapon(bool swapNext)
    {
        // find index of weapon to swap to, wrapping if necessary
        int swapDirection = (swapNext ? 1 : -1);
        int newWeaponIndex = playerFire.CurrentWeapon.transform.GetSiblingIndex();
        do
        {
            newWeaponIndex += swapDirection;
            if (newWeaponIndex >= transform.childCount || newWeaponIndex < 0)
                newWeaponIndex += transform.childCount * ((newWeaponIndex < 0) ? 1 : -1);
        }
        while (!transform.GetChild(newWeaponIndex).gameObject.activeSelf);

        // swap weapon, play sound, and update cursor
        playerFire.CurrentWeapon = transform.GetChild(newWeaponIndex).GetComponent<Weapon>();
        AudioManager.Play(mySwapSound, true);
        CursorManager.Instance.SetCursor((Cursors)(newWeaponIndex + 1));

        // invoke event to update current weapon on UI
        updateCurrentWeapon.Invoke(newWeaponIndex);
    }

    /// <summary>
    /// Directly selects weapon of given type if player
    /// has ammunition for it
    /// </summary>
    /// <param name="switchTo">index of weapon type to switch to</param>
    void SelectWeapon(int switchToIndex)
    {
        // if child weapon object is active
        if (transform.GetChild(switchToIndex).gameObject.activeSelf)
        {
            // swap weapon, update cursor, and play sound
            playerFire.CurrentWeapon = transform.GetChild(switchToIndex).GetComponent<Weapon>();
            updateCurrentWeapon.Invoke(switchToIndex);
            CursorManager.Instance.SetCursor((Cursors)(switchToIndex + 1));
            AudioManager.Play(mySwapSound, true);
        }
        // otherwise (player lacks ammo for given weapon type)
        else
        {
            // play denied sound effect
            AudioManager.Play(AudioClipNames.UI_denied, true);
        }
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
    }

    /// <summary>
    /// Handles empty weapon event, swapping current weapon to
    /// previous under player. This method is safe as the first
    /// weapon (pistol) will always have infinite ammo.
    /// </summary>
    void HandleEmptyWeapon()
    {
        SwapWeapon(false);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given method as listener to swap weapon UI event
    /// </summary>
    /// <param name="newListener">new listener method for event</param>
    public void AddSwapWeaponUIListener(UnityAction<int> newListener)
    {
        updateCurrentWeapon.AddListener(newListener);
    }

    #endregion

}
