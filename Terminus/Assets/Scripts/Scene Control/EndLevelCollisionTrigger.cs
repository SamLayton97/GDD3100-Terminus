using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger which elects to end level when player collides with it
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class EndLevelCollisionTrigger : LevelEnder
{
    // public variables
    public bool successOnCollision = true;              // flag determining whether collision ends in success or failure

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // set object's collider to be a trigger if not already
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        if (!myCollider.isTrigger)
            myCollider.isTrigger = true;
    }

    /// <summary>
    /// Elects to end level when player's non-trigger collider meets
    /// this object's trigger collider
    /// </summary>
    /// <param name="other">other object in collision</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if object is on Player layer and player's non-trigger collider met with object
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !other.isTrigger)
            endLevelEvent.Invoke(successOnCollision, 
                other.GetComponent<SanityControl>().CurrentSanity * (successOnCollision ? 1 : 0));
    }
}
