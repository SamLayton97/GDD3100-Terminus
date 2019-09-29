﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi-automatic weapon which fires a single
/// "corrupted"/poisoned bullet each time it registers
/// fire input
/// </summary>
public class BioRifle : Weapon
{
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

            // TODO: reduce player's sanity by set amount

            // TODO: play random fire sound effect

            // play firing animation
            myAnimator.SetBool("isShooting", true);
            myAnimator.Play("ShootAnimation", -1, 0);
        }
    }
}
