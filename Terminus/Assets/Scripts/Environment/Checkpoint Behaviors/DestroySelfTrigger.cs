using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys checkpoint when player collides with it.
/// </summary>
public class DestroySelfTrigger : MonoBehaviour
{
    /// <summary>
    /// Called when player enters trigger box
    /// </summary>
    /// <param name="other">collision info</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if player's body entered trigger
        if (!other.isTrigger)
        {
            // self-destruct
            Destroy(gameObject);
        }
    }
}
