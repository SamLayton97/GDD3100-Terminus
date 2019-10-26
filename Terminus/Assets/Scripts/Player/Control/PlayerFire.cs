using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/// <summary>
/// Registers user's fire input, firing weapon/tool in direction
/// of mouse and sending player in opposite direction.
/// Note: Requires player character to have the Rigidbody2D component.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFire : MonoBehaviour
{
    // private variables
    Weapon currWeapon = null;           // current weapon wielded by player character
    bool firedLastFrame = false;        // flag determining whether player fired weapon on last Update() (helps with semi-automatic weapon firing)

    // configuration variables
    [SerializeField] float fireShakeMagnitude = 1.25f;      // magnitude of screen shake when player fires weapon
    [SerializeField] float fireShakeRoughness = 0.8f;       // roughness of screen shake when player fires weapon

    /// <summary>
    /// Public read/write-access property returning
    /// weapon player is currently wielding
    /// </summary>
    public Weapon CurrentWeapon
    {
        get { return currWeapon; }
        set { currWeapon = value; }
    }

    // Used for initialization
    void Awake()
    {
        // if current weapon was not set in inspector, retrieve first weapon component in children
        if (currWeapon == null)
            currWeapon = GetComponentInChildren<Weapon>();
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
            CameraShaker.Instance.ShakeOnce(fireShakeMagnitude * ((currWeapon.myType != WeaponType.Shotgun) ? 1f : 2f)
                , fireShakeRoughness * ((currWeapon.myType != WeaponType.Shotgun) ? 1f : 2f), 0.1f, 0.1f);

            // TODO: brighten player for a frame

        }
        // otherwise (no input registered), reset fired last frame flag
        else
            firedLastFrame = false;
    }
}
