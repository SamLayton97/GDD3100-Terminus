using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls area-of-effect done to enemies. Typically used by 
/// explosions created by projectiles who burst on contact/over time.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class AreaOfEffect : MonoBehaviour
{
    // private variables
    CircleCollider2D myTriggerCollider;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve collider and ensure that it is a trigger
        myTriggerCollider = GetComponent<CircleCollider2D>();
        myTriggerCollider.isTrigger = true;
    }

    /// <summary>
    /// Destroys explosion when it's animation ends
    /// </summary>
    public void DestroyOnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
