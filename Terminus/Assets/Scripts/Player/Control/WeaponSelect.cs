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
[RequireComponent(typeof(SpriteRenderer))]
public class WeaponSelect : MonoBehaviour
{
    // weapon container support
    [SerializeField] Transform weaponContainer;               // child game object holding all of player's weapons

    // private variables
    PlayerFire playerFire;                                  // player fire component (gets its current weapon property updated)
    SpriteRenderer myRenderer;                              // player object's sprite renderer -- used to change its HSV on weapon swap
    AudioClipNames mySwapSound =                            // sound played when player swaps to different weapon
        AudioClipNames.player_swapWeapon;
    Vector4 standardHSV = new Vector4();                    // standard HSV of player's color tint shader
    IEnumerator colorSwap;                                  // coroutine controlling color swap of player sprite

    // event support
    SwapWeaponUIEvent updateCurrentWeapon;                  // event invoked to update player's current weapon on HUD

    // configuration variables
    [SerializeField] GameObject[] allWeapons;               // serialized array of all weapons objects player could have
                                                            // NOTE: must be populated in order they appear in enumeration
    [SerializeField] List<Vector4> swapHSVs =               // list of HSV colors player sprite swaps to when changing weapons
        new List<Vector4>();                                // NOTE: must be populated in order they appear in enumeration
    [SerializeField] Vector4 deniedSwapHSV;                 // HSV of shader when player fails to swap weapon (no ammo)
    [Range(1, 60)]
    [SerializeField] int swapDuration = 2;                  // number of frames sprite tint remains swapped

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components/information
        playerFire = GetComponent<PlayerFire>();
        myRenderer = GetComponent<SpriteRenderer>();
        standardHSV = myRenderer.material.GetVector("_HSVAAdjust");

        // if not set before startup, grab transform of first child gameobject (assume to be weapons holder)
        if (!weaponContainer)
            weaponContainer = transform.GetChild(0);

        // for all limited-ammo weapons, spawn them under player but as inactive
        for (int i = 1; i < allWeapons.Length; i++)
            Instantiate(allWeapons[i], weaponContainer).SetActive(false);
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
        EventManager.AddHUDSelectWeaponListener(SelectWeapon);
        EventManager.AddHUDSwapWeaponListener(SwapWeapon);

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
            if (newWeaponIndex >= weaponContainer.childCount || newWeaponIndex < 0)
                newWeaponIndex += weaponContainer.childCount * ((newWeaponIndex < 0) ? 1 : -1);
        }
        while (!weaponContainer.GetChild(newWeaponIndex).gameObject.activeSelf);

        // swap weapon, play sound, and update cursor
        playerFire.CurrentWeapon = weaponContainer.GetChild(newWeaponIndex).GetComponent<Weapon>();
        AudioManager.Play(mySwapSound, true);
        CursorManager.Instance.SetCursorType((Cursors)(newWeaponIndex + 1));

        // start coroutine changing sprite HSV
        StartCoroutine(ColorSwap(swapHSVs[newWeaponIndex], swapDuration));

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
        if (weaponContainer.GetChild(switchToIndex).gameObject.activeSelf)
        {
            // swap weapon, update cursor, and play sound
            playerFire.CurrentWeapon = weaponContainer.GetChild(switchToIndex).GetComponent<Weapon>();
            updateCurrentWeapon.Invoke(switchToIndex);
            CursorManager.Instance.SetCursorType((Cursors)(switchToIndex + 1));
            AudioManager.Play(mySwapSound, true);

            // start coroutine to changing sprite HSV
            StartCoroutine(ColorSwap(swapHSVs[switchToIndex], swapDuration));
        }
        // otherwise (player lacks ammo for given weapon type)
        else
        {
            // play denied audio-visual feedback
            AudioManager.Play(AudioClipNames.UI_denied, true);
            StartCoroutine(ColorSwap(deniedSwapHSV, swapDuration));
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
        if (!weaponContainer.GetChild((int)newWeapon).gameObject.activeSelf)
            weaponContainer.GetChild((int)newWeapon).gameObject.SetActive(true);

        // refill corresponding weapon's ammo
        weaponContainer.GetChild((int)newWeapon).GetComponent<Weapon>().RefillAmmo();
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

    /// <summary>
    /// Changes color of player for n-number of frames.
    /// Used when player attempts to change their weapon.
    /// </summary>
    /// <param name="frameHSV">HSV sprite changes to for frame</param>
    /// <param name="frames">number of frames color is swapped</param>
    /// <returns></returns>
    IEnumerator ColorSwap(Vector4 frameHSV, int frames)
    {
        // swap color
        myRenderer.material.SetVector("_HSVAAdjust", frameHSV);

        // reset color after n frames
        int frameCount = 0;
        while (frameCount < frames)
        {
            frameCount++;
            yield return new WaitForEndOfFrame();
        }
        myRenderer.material.SetVector("_HSVAAdjust", standardHSV);
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
