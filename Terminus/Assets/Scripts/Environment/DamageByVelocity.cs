using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Defines 'hardness' of environmental object, affecting how fast
/// player must be going to be harmed by wall and how much it will 
/// harm them.
/// </summary>
public class DamageByVelocity : O2Remover
{
    // public variables
    public float minVelocityToDamage = 10f;     // minimum velocity player must collide with wall to receive damage
    public float damageScalar = 1f;             // scale by which damage increases as velocity increases (linear)

    /// <summary>
    /// Calculates damage done to player on collision
    /// </summary>
    /// <param name="velocity">velocity of player on collision</param>
    float CalculateDamage(Vector2 velocity)
    {
        return Mathf.Max(0, velocity.magnitude - minVelocityToDamage) * damageScalar;
    }

    /// <summary>
    /// Applies damage to player on significant impact
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if other object in collision resides on player layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            Debug.Log("hit player");
    }
}
