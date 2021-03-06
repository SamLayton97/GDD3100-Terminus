﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior which deals damage to agent over time.
/// Note: Requires agent to have health.
/// </summary>
[RequireComponent(typeof(AgentHealth))]
[RequireComponent(typeof(SpriteRenderer))]
public class PoisonAgent : AgentStatusEffect
{
    // private variables
    AgentHealth myHealth;                       // agent's health component (used to damage agent over time)
    SpriteRenderer mySpriteRenderer;            // agent's sprite renderer component (used for visual feedback)
    PursueAndAttack myBehavior;                 // component controlling agent's state-machine behaviors
    float damagePerDeduction = 5f;              // amount of damage dealt to agent on each call of Deduct Health() (Note: Often set by poisoner)
    float timeBetweenDeductions = 1f;           // time between calls of agent's Deduct Health() method
    float poisonTimer = 0;                      // helps track when to hurt agent
    Color32 poisonColor =                       // color agent gradually transitions to while poisoned
        new Color32(0x32, 0xB7, 0x4B, 0xFF);
    Vector4 poisonHSV =                         // HSV agent's material gradually transitions to while poisoned
        new Vector4(0.2f, 0.15f, -0.25f);

    /// <summary>
    /// Write-access property for how much damage
    /// behavior deals to agent when timer hits 0
    /// </summary>
    public float DamagePerDeduction
    {
        set { damagePerDeduction = value; }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myHealth = GetComponent<AgentHealth>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBehavior = GetComponent<PursueAndAttack>();

        // initialize poison timer
        poisonTimer = timeBetweenDeductions;

        // reduce agent's speed
        myBehavior.AgentSpeed *= 0.85f;
    }

    // Update is called once per frame
    void Update()
    {
        // lerp agent's HSV to poisoned color
        //mySpriteRenderer.material.SetVector("_HSVAAdjust",
        //    Vector4.Lerp(mySpriteRenderer.material.GetVector("_HSVAAdjust"), poisonHSV, Time.deltaTime));
        mySpriteRenderer.material.SetVector("_HSVAAdjust", poisonHSV);

        // decrement timer
        poisonTimer -= Time.deltaTime;

        // if timer hits 0, deduct health and reset timer
        if (poisonTimer <= 0)
        {
            myHealth.DeductHealth(damagePerDeduction);
            poisonTimer = timeBetweenDeductions;
        }
    }
}
