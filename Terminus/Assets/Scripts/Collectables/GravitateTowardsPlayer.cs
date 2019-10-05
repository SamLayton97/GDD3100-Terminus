using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves object towards player once within specified range
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GravitateTowardsPlayer : MonoBehaviour
{
    // serialized variables
    [Range(0, 10f)]
    [SerializeField] float gravitationSpeed = 3f;       // speed at which object gravitates towards player

    // private variables
    Rigidbody2D myRigidbody2D;                          // object's rigidbody2d component (used to change object's velocity)

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // ensure object's circle collider is a trigger
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    /// <summary>
    /// Called each frame an object is within trigger collider
    /// </summary>
    /// <param name="collider">collision data</param>
    void OnTriggerStay2D(Collider2D collider)
    {
        // ensure object is on the player layer and collider is player's body
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") &&
            !collider.isTrigger)
        {
            // move towards player
            myRigidbody2D.velocity = (collider.transform.position - transform.position).normalized * gravitationSpeed;
        }
    }
}
