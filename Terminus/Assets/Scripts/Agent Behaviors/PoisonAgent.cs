using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior which deals damage to agent over time.
/// Note: Requires agent to have health.
/// </summary>
[RequireComponent(typeof(AgentHealth))]
[RequireComponent(typeof(SpriteRenderer))]
public class PoisonAgent : MonoBehaviour
{
    // private variables
    AgentHealth myHealth;                       // agent's health component (used to damage agent over time)
    SpriteRenderer mySpriteRenderer;            // agent's sprite renderer component (used for visual feedback)
    float damagePerDeduction = 5f;              // amount of damage dealt to agent on each call of Deduct Health() (Note: Often set by poisoner)
    float timeBetweenDeductions = 2f;           // time between calls of agent's Deduct Health() method
    float poisonTimer = 0;                      // helps track when to hurt agent
    Color32 poisonColor =                       // color agent gradually transitions to while poisoned
        new Color32(0x32, 0xB7, 0x4B, 0xFF);

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
        Debug.Log("Poisoned");

        // retrieve necessary components
        myHealth = GetComponent<AgentHealth>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // initialize poison timer
        poisonTimer = timeBetweenDeductions;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: lerp agent's color to dark green
        mySpriteRenderer.color = Color.Lerp(mySpriteRenderer.color, poisonColor, Time.deltaTime);

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
