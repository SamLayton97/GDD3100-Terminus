﻿using System.Collections;
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
    public float maxHealth = 15f;               // starting health of enemy agent
    public Vector4 deathHSV;                    // HSV to darken agent's material to on death
    public GameObject damagePartcileEffect;

    // sound effect support
    public AudioClipNames myDeathSound = AudioClipNames.agent_chaserDeath;  // sound played when agent is killed
    public AudioClipNames[] myHurtSounds =                                  // collection of sounds played when agent gets hurt
{
        AudioClipNames.agent_chaserHurt,
        AudioClipNames.agent_chaserHurt1,
        AudioClipNames.agent_chaserHurt2
    };

    // private variables
    bool softDisabled = false;              // flag determining whether agent has been disabled (used for handling death)
    float currHealth;                       // current health of agent
    SpriteRenderer mySpriteRenderer;        // agent's sprite renderer component (used to set color of sprite)
    Animator myAnimator;                    // animator used to play agent's death animation (if it has one)
    O2Remover myBehavior;                   // component controlling agent's hostile behavior
    DropMaterialOnDeath myMaterialDropper;  // component used to drop crafting materials on death

    #region Unity Methods

    /// <summary>
    /// Used for internal initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myBehavior = GetComponent<O2Remover>();
        myMaterialDropper = GetComponent<DropMaterialOnDeath>();

        // initialize health
        currHealth = maxHealth;
    }

    /// <summary>
    /// Called when agent makes contact with collidable object
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if other object in collision is a player projectile/photon
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles") ||
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerPhotons"))
        {
            // deduct health
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            DeductHealth(projectile.Damage);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles all necessary processes related to handling agent's death
    /// </summary>
    void HandleAgentDeath()
    {
        // if agent hasn't already been soft-disabled
        if (!softDisabled)
        {
            // set animation triggers
            myAnimator.SetTrigger("OnDeathTrigger");

            // if component exists, drop agent's crafting materials
            if (myMaterialDropper != null)
                myMaterialDropper.DropMaterials();

            // play agent's death sound effect
            AudioManager.Play(myDeathSound, true);

            // soft-disable agent
            softDisabled = true;
            myBehavior.enabled = false;
            mySpriteRenderer.material.SetVector("_HSVAAdjust", deathHSV);
            gameObject.layer = LayerMask.NameToLayer("Corpses");
            mySpriteRenderer.sortingLayerName = "Corpses";

            // disable agent's status effects
            AgentStatusEffect[] statusEffects = GetComponents<AgentStatusEffect>();
            for (int i = 0; i < statusEffects.Length; i++)
                statusEffects[i].enabled = false;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Deducts set amount from agent's health pool,
    /// killing them if necessary.
    /// </summary>
    /// <param name="damage">damage dealt to agent</param>
    public void DeductHealth(float damage)
    {
        // deduct health
        currHealth -= damage;

        // instantiate damage effect at agent's position
        Instantiate(damagePartcileEffect, transform.position, Quaternion.identity);

        // kill agent if health falls below 0
        if (currHealth <= 0)
            HandleAgentDeath();
        // otherwise, play standard hurt sound effect
        else
            AudioManager.Play(myHurtSounds[Random.Range(0, myHurtSounds.Length)], true);
    }

    #endregion

}
