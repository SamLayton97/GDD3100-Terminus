using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int currAmmo = 100;                              // current ammunution stored in weapon
    public int maxAmmo = 100;                               // max amount of ammunition able to be stored in weapon
    public Vector2 bulletInstanceOffset;                    // offset to spawn bullets at (useful when bullets should spawn from tip of gun rather than center of object)

    // protected variables
    protected bool firedLastFrame = false;      // flag determining whether weapon registered a shot on the previous frame
    protected Rigidbody2D parentRigidbody;      // rigidbody 2d component of agent firing weapon
    protected Animator myAnimator;              // animation component used to play firing animation
    
    /// <summary>
    /// Registers shot when user fires their weapon.
    /// Note: All weapons must do something when user fires weapon.
    /// </summary>
    /// <param name="firedLastFrame">whether player fired on previous frame</param>
    public abstract void RegisterInput(bool firedLastFrame);

    /// <summary>
    /// Stops firing animation when it ends
    /// </summary>
    public void StopAnimation()
    {
        myAnimator.SetBool("isShooting", false);
    }

    /// <summary>
    /// Called on initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        parentRigidbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

}
