using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Semi-automatic weapon which fires a single
/// "corrupted"/poisoned bullet each time it registers
/// fire input
/// </summary>
public class BioRifle : Weapon
{
    // public variables
    public float sanityLostOnShot = 10f;    // amount of sanity player looses when they fire weapon

    // event support
    DeductSanityOnFire deductSanityEvent;

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    void Start()
    {
        // add self as invoker of deduct sanity on fire event
        deductSanityEvent = new DeductSanityOnFire();
        EventManager.AddDeductSanityOnFireInvoker(this);
    }

    /// <summary>
    /// Fires a bio-projectile, applying reactionary force to user
    /// and reducing their sanity
    /// </summary>
    /// <param name="firedLastFrame">whether user of weapon fired on last frame</param>
    public override void RegisterInput(bool firedLastFrame)
    {
        // if player didn't fire last frame, register fire input
        if (!firedLastFrame)
        {
            // fire pistol shot in direction of weapon's rotation
            float agentRotation = transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 fireVector = new Vector2(Mathf.Cos(agentRotation), Mathf.Sin(agentRotation)).normalized;
            GameObject newProjectile = Instantiate(projectileObject, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().AddForce((fireVector * projectileForce) + parentRigidbody.velocity,
                ForceMode2D.Impulse);
            newProjectile.GetComponent<FaceVelocity>().RelativeTo = parentRigidbody;

            // apply reactive force to weapon user in opposite direction
            parentRigidbody.AddForce((fireVector * -1 * reactiveForce), ForceMode2D.Impulse);

            // reduce player's sanity by set amount
            deductSanityEvent.Invoke(sanityLostOnShot);

            // play random fire sound effect
            AudioManager.Play(myFireSounds[Random.Range(0, myFireSounds.Length)], true);

            // play firing animation
            myAnimator.SetBool("isShooting", true);
            myAnimator.Play("ShootAnimation", -1, 0);
        }
    }

    /// <summary>
    /// Adds given method as listener to deduct sanity on fire event
    /// </summary>
    /// <param name="newListener">new listener of event</param>
    public void AddDeductSanityInvoker(UnityAction<float> newListener)
    {
        deductSanityEvent.AddListener(newListener);
    }
}
