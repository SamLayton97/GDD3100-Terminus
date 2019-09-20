using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls health status and death of enemy agent
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AgentHealth : MonoBehaviour
{
    // public variables
    public float maxHealth = 15f;       // starting health of enemy agent
    public Color deathColor;            // color to darken agent to on death

    // private variables
    float currHealth;                   // current health of agent
    SpriteRenderer mySpriteRenderer;    // agent's sprite renderer component (used to set color of sprite)
    Animator myAnimator;                // animator used to play agent's death animation (if it has one)
    O2Remover myBehavior;               // component controlling agent's hostile behavior

    /// <summary>
    /// Used for internal initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myBehavior = GetComponent<O2Remover>();

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
            if (currHealth <= 0) HandleAgentDeath();
        }
    }

    /// <summary>
    /// Handles all necessary processes related to handling agent's death
    /// </summary>
    void HandleAgentDeath()
    {
        // set animation triggers
        myAnimator.SetTrigger("OnDeathTrigger");

        // soft-disable agent
        myBehavior.enabled = false;
        mySpriteRenderer.color = deathColor;
        gameObject.layer = LayerMask.NameToLayer("Environment");
        mySpriteRenderer.sortingLayerName = "MiscellaneousObjects";
    }
}
