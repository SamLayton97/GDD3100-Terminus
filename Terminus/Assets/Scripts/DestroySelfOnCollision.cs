using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys this game object on collision with another collidable object.
/// Typically used for projectiles and in-environment collectables.
/// </summary>
public class DestroySelfOnCollision : MonoBehaviour
{
    /// <summary>
    /// Destroys game object on first contact with other collidable object.
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
