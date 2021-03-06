﻿using System.Collections;
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
    public float damageConstant = 0f;           // constant damage applied regardless of velocity
    public AudioClipNames myCollisionSound =    // sound played when player collides with object at high enough velocity
        AudioClipNames.env_playerWallCollide;

    /// <summary>
    /// Calculates damage done to player on collision
    /// </summary>
    /// <param name="velocity">velocity of player on collision</param>
    float CalculateDamage(Vector2 velocity)
    {
        return Mathf.Max(0, velocity.magnitude - minVelocityToDamage) * damageScalar + damageConstant;
    }

    /// <summary>
    /// Applies damage to player on significant impact
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if other object in collision resides on player layer 
        // and relative velocity meets minimum needed to damage
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && 
            collision.relativeVelocity.magnitude >= minVelocityToDamage)
        {
            // deduct oxygen and play sound effect
            deductO2Event.Invoke(CalculateDamage(collision.relativeVelocity), true);
            AudioManager.Play(myCollisionSound, true);
        }
    }
}
