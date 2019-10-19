using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns particles on collision with a given physics layer
/// </summary>
public class ParticlesOnCollision : MonoBehaviour
{
    // particle collision support
    [SerializeField] GameObject particleEffect;         // particle effect object instantiated on collision
    [SerializeField] LayerMask collisionLayermask;          // physics layer(s) which cause particles to spawn on collision

    /// <summary>
    /// Called upon entering collision with another object
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if collision occurred on accepted layer(s), spawn particle effect
        if (collisionLayermask == (collisionLayermask | (1 << collision.gameObject.layer)))
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }
    }
}
