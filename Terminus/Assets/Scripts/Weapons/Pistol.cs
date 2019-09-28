using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple semi-automatic pistol used by player
/// </summary>
public class Pistol : Weapon
{
    /// <summary>
    /// Fires a pistol shot, applying reactionary force to agent who shot weapon
    /// </summary>
    /// <param name="firedLastFrame">whether user of weapon fired on last frame</param>
    public override void RegisterInput(bool firedLastFrame)
    {
        // if player didn't fire last frame, register a shot
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

            // play random firing sound
            AudioManager.Play(myFireSounds[Random.Range(0, myFireSounds.Length)], true);

            // play firing animation
            myAnimator.SetBool("isShooting", true);
            myAnimator.Play("ShootAnimation", -1, 0);
        }
    }
}
