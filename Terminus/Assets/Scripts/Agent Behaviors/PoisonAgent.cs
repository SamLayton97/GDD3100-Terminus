using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior which deals damage to agent over time.
/// Note: Requires agent to have health.
/// </summary>
[RequireComponent(typeof(AgentHealth))]
public class PoisonAgent : MonoBehaviour
{
    // private variables
    AgentHealth myHealth;               // agent's health component (used to damage agent over time)
    float damagePerDeduction = 5f;      // amount of damage dealt to agent on each call of Deduct Health() (Note: Often set by poisoner)
    float timeBetweenDeductions = 2f;   // time between calls of agent's Deduct Health() method
    float poisonTimer = 0;              // helps track when to hurt agent
    
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

        // initialize poison timer
        poisonTimer = timeBetweenDeductions;
    }

    // Update is called once per frame
    void Update()
    {
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
