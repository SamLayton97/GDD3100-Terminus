using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns particles on collision with a given physics layer
/// </summary>
public class ParticlesOnCollision : MonoBehaviour
{
    // particle collision variables
    [SerializeField] GameObject particleEffect;         // particle effect object instantiated on collision
    [SerializeField] LayerMask collisionLayermask;      // physics layer(s) which cause particles to spawn on collision

    // scaling variables
    [SerializeField] bool scaleWithMagnitude = false;       // flag determining whether particle effect should scale with force magnitude of collision
    [SerializeField] Vector2 scaleBounds = new Vector2();   // lower/upper bounds which particle effect can scale within
    [SerializeField] float scaleRate = 1f;                  // rate at which particle effect scales with force of collision

    /// <summary>
    /// Called upon entering collision with another object
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if collision occurred on accepted layer(s)
        if (collisionLayermask == (collisionLayermask | (1 << collision.gameObject.layer)))
        {
            // spawn particle effect
            GameObject newParticle = Instantiate(particleEffect, transform.position, Quaternion.identity);

            // scale particle effect by collision magnitude if appropriate
            if (scaleWithMagnitude)
            {
                // NOTE: scaling mode must be Hierarchy to behave appropriately
                ParticleSystem.MainModule main = newParticle.GetComponent<ParticleSystem>().main;
                main.scalingMode = ParticleSystemScalingMode.Hierarchy;
                newParticle.transform.localScale *= 
                    Mathf.Clamp(collision.relativeVelocity.magnitude * scaleRate, scaleBounds.x, scaleBounds.y);
            }
        }
    }
}
