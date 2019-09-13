using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Trigger which elects to transition to specified scene when player
/// collides with it.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class TransitionSceneCollisionTrigger : SceneTransitioner
{
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
    /// Elects to transition to new scene when player
    /// object passes through this object's trigger collider.
    /// </summary>
    /// <param name="other">other object in collision</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: transition to new scene

    }
}
