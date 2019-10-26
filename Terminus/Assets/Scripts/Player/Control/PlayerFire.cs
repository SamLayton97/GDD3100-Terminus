using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/// <summary>
/// Registers user's fire input, firing weapon/tool in direction
/// of mouse and sending player in opposite direction.
/// Note: Requires player character to have the Rigidbody2D component.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFire : MonoBehaviour
{
    // support variables
    Weapon currWeapon = null;               // current weapon wielded by player character
    bool firedLastFrame = false;            // flag determining whether player fired weapon on last Update() (helps with semi-automatic weapon firing)
    SpriteRenderer myRenderer;              // player's sprite renderer (used to adjust their HSV)
    Vector4 standardHSV = new Vector4();    // HSV of player's shader under no special conditions

    // configuration variables
    [SerializeField] float fireShakeMagnitude = 1.25f;      // magnitude of screen shake when player fires weapon
    [SerializeField] float fireShakeRoughness = 0.8f;       // roughness of screen shake when player fires weapon
    [SerializeField] Vector4 fireHSV = new Vector4();

    /// <summary>
    /// Public read/write-access property returning
    /// weapon player is currently wielding
    /// </summary>
    public Weapon CurrentWeapon
    {
        get { return currWeapon; }
        set { currWeapon = value; }
    }

    #region Unity Methods

    // Used for initialization
    void Awake()
    {
        // retrieve necessary components from self and children
        myRenderer = GetComponent<SpriteRenderer>();
        if (currWeapon == null)
            currWeapon = GetComponentInChildren<Weapon>();

        // retrieve starting HSV
        standardHSV = myRenderer.material.GetVector("_HSVAAdjust");
    }

    // Update is called once per frame
    void Update()
    {
        // if player fires weapon and game isn't paused
        if (CustomInputManager.GetMouseButton("Fire") && Time.timeScale != 0)
        {
            // register input in weapon and set fired last frame flag to true
            currWeapon.RegisterInput(firedLastFrame);
            firedLastFrame = true;

            // shake screen, scaling magnitude and roughness by weapon type
            CameraShaker.Instance.ShakeOnce(fireShakeMagnitude * ((currWeapon.myType != WeaponType.Shotgun) ? 1f : 2f), 
                fireShakeRoughness * ((currWeapon.myType != WeaponType.Shotgun) ? 1f : 2f), 0.1f, 0.1f);

            // brighten player for a frame
            StartCoroutine(BrightenPlayer());

        }
        // otherwise (no input registered), reset fired last frame flag
        else
            firedLastFrame = false;
    }

    #endregion

    #region Coroutines

    /// <summary>
    /// Brightens player for a single frame. Used when
    /// player fires their weapon.
    /// </summary>
    /// <returns>coroutine ending on next frame</returns>
    IEnumerator BrightenPlayer()
    {
        // brighten player, returning to normal after a frame
        myRenderer.material.SetVector("_HSVAAdjust", fireHSV);
        yield return new WaitForEndOfFrame();
        myRenderer.material.SetVector("_HSVAAdjust", standardHSV);
    }


    #endregion
}
