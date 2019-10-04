using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes projectile to explode on collision. Typically used
/// by projectiles who have area of effect (e.g., bio projectiles).
/// </summary>
public class ExplodeOnContact : MonoBehaviour
{
    // public variables
    public GameObject myExplosion;

    /// <summary>
    /// Called when object's collider meets with another
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: create explosion
        Debug.Log("Boom!");

        // destroy self
        Destroy(gameObject);
    }
}
