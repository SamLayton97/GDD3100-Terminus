﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class of all weapons, which spawn some projectile
/// and apply a force to the user in the opposite direction.
/// </summary>
[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    // public variables
    public GameObject projectileObject = null;              // game object weapon fires
    public AudioClipNames[] myFireSounds;                   // sound played when weapon is fired
    public bool continuousFiring = false;                   // whether user may fire weapon for consecutive frames
    public float projectileForce = 5.0f;                    // force by which object propels projectile
    public float reactiveForce = 2.5f;                      // force by which object propels user in opposite direction
    [Range(-1, 20)]
    public int maxAmmo = 10;                                // max amount of ammunition able to be stored in weapon (-1 denotes weapon has unlimited ammo)
    public Vector2 bulletInstanceOffset;                    // offset to spawn bullets at (useful when bullets should spawn from tip of gun rather than center of object)

    // protected variables
    protected bool firedLastFrame = false;      // flag determining whether weapon registered a shot on the previous frame
    protected Rigidbody2D parentRigidbody;      // rigidbody 2d component of agent firing weapon
    protected Animator myAnimator;              // animation component used to play firing animation

    // private variables
    int currAmmo = 0;                           // current ammo stored in weapon (weapon is destroyed if 0)

    // event support
    EmptyWeaponEvent emptyWeaponEvent;          // event invoked when this weapon runs out of ammo

    #region Unity Methods

    /// <summary>
    /// Called on initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        parentRigidbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        // initialize ammo counter
        currAmmo = maxAmmo;
    }

    /// <summary>
    /// Called before first frame Update
    /// </summary>
    protected virtual void Start()
    {
        // add self as invoker of empty weapon event
        emptyWeaponEvent = new EmptyWeaponEvent();
        EventManager.AddEmptyWeaponInvoker(this);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Registers shot when user fires their weapon.
    /// </summary>
    /// <param name="firedLastFrame">whether player fired on previous frame</param>
    public virtual void RegisterInput(bool firedLastFrame)
    {
        // if player didn't fire last frame, register a shot
        if (!firedLastFrame)
        {
            // fire projectile in direction of weapon's rotation
            float agentRotation = transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 fireVector = new Vector2(Mathf.Cos(agentRotation), Mathf.Sin(agentRotation)).normalized;
            GameObject newProjectile = Instantiate(projectileObject, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().AddForce((fireVector * projectileForce) + parentRigidbody.velocity,
                ForceMode2D.Impulse);
            newProjectile.GetComponent<FaceVelocity>().RelativeTo = parentRigidbody;

            // apply reactive force to weapon user in opposite direction
            parentRigidbody.AddForce((fireVector * -1 * reactiveForce), ForceMode2D.Impulse);

            // decrement ammo
            DecrementRemainingAmmo();

            // play random firing sound
            AudioManager.Play(myFireSounds[Random.Range(0, myFireSounds.Length)], true);

            // play firing animation
            myAnimator.SetBool("isShooting", true);
            myAnimator.Play("ShootAnimation", -1, 0);
        }
    }

    /// <summary>
    /// Refills weapon's ammo to its maxs
    /// </summary>
    public void RefillAmmo()
    {
        currAmmo = maxAmmo;
    }

    /// <summary>
    /// Stops firing animation when it ends
    /// </summary>
    public void StopAnimation()
    {
        myAnimator.SetBool("isShooting", false);
    }

    /// <summary>
    /// Adds given method as listener to Empty Weapon event
    /// </summary>
    /// <param name="newListener">new listener method for event</param>
    public void AddEmptyWeaponListener(UnityAction newListener)
    {
        emptyWeaponEvent.AddListener(newListener);
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Decrements remaining ammo, destroying object if empty
    /// </summary>
    protected void DecrementRemainingAmmo()
    {
        // if weapon doesn't have infinite ammo
        if (maxAmmo != -1)
        {
            // decrement ammo, destroying weapon if empty
            currAmmo--;
            if (currAmmo < 1)
            {
                Destroy(gameObject);
                emptyWeaponEvent.Invoke();
            }
        }
    }

    #endregion

}
