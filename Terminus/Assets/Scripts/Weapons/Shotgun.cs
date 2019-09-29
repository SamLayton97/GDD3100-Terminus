using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi-automatic shotgun used by player to 
/// fire several projectiles within an arc
/// </summary>
public class Shotgun : Weapon
{
    // public variables
    [Range(0, 10)]
    public int projectilesInBlast = 5;      // number of projectiles created in shotgun blast
    [Range(0, 180)]
    public float fireArc = 45f;             // arc within which projectiles can travel at a random angle

    /// <summary>
    /// Fires several projectiles within an arc, applying a large
    /// reactionary force to agent who shot weapon
    /// </summary>
    /// <param name="firedLastFrame"></param>
    public override void RegisterInput(bool firedLastFrame)
    {
        // if player didn't fire last frame, register a shot
        if (!firedLastFrame)
        {
            // get angle of agent firing weapon
            float agentRotation = transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad;

            // for the number of projectiles in a shotgun blast
            for (int i = 0; i < projectilesInBlast; i++)
            {
                // fire projectile in direction of weapon plus random offset
                float offsetWithinArc = Random.Range(fireArc * -0.5f, fireArc * 0.5f) * Mathf.Deg2Rad;
                Debug.Log(offsetWithinArc);
                Vector2 fireVector = new Vector2(Mathf.Cos(agentRotation + offsetWithinArc), Mathf.Sin(agentRotation + offsetWithinArc)).normalized;
                GameObject newProjectile = Instantiate(projectileObject, transform.position, Quaternion.identity);
                newProjectile.GetComponent<Rigidbody2D>().AddForce((fireVector * projectileForce) + parentRigidbody.velocity,
                    ForceMode2D.Impulse);
                newProjectile.GetComponent<FaceVelocity>().RelativeTo = parentRigidbody;
            }

            // apply reactive force to weapon user in opposite direction
            parentRigidbody.AddForce((new Vector2(Mathf.Cos(agentRotation), Mathf.Sin(agentRotation)) * -1 * reactiveForce), ForceMode2D.Impulse);

            // TODO: play shotgun blast sound effect

            // play shotgun firing animation
            myAnimator.SetBool("isShooting", true);
            myAnimator.Play("ShootAnimation", -1, 0);
        }
    }
}
