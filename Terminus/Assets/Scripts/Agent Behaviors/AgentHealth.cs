using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls health status and death of enemy agent
/// </summary>
public class AgentHealth : MonoBehaviour
{
    // public variables
    public float maxHealth = 15f;     // starting health of enemy agent

    // private variables
    float currHealth;       // current health of agent

    /// <summary>
    /// Used for internal initialization
    /// </summary>
    void Awake()
    {
        // initialize health
        currHealth = maxHealth;
    }

    /// <summary>
    /// Called when agent makes contact with collidable object
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if other object in collision is a projectile
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
        {
            // deduct health and destroy gameobject if necessary
            currHealth -= collision.gameObject.GetComponent<Projectile>().Damage;
            if (currHealth <= 0) Destroy(gameObject);
        }
    }
}
