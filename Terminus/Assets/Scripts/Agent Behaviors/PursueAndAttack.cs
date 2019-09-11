﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy agent behavior of pursuing player once within
/// sight range and attacking within melee distance.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PursueAndAttack : MonoBehaviour
{
    // state enumeration
    public enum ChaseStates
    {
        Idle,
        Pursue,
        AttackAndWait
    }

    // public variables
    public ChaseStates startingState = ChaseStates.Idle;    // state which pursuing agent starts in (typically Idle)
    public Transform targetTransform;                       // transform of target to pursue (typically player)
    public float speed = 5f;                                // magnitude of agent's velocity
    public float sightRange = 30f;                          // max distance agent can see target without objects obstructing its view

    // private variables
    ChaseStates currState;

    #region State Machine

    /// <summary>
    /// Updates agent as it waits idly
    /// </summary>
    void UpdateIdle()
    {
        Debug.Log(CanSeeTarget());
    }

    /// <summary>
    /// Updates agent as it pursues its target
    /// </summary>
    void UpdatePursue()
    {
        Debug.Log("Pursue");
    }

    /// <summary>
    /// Updates agent as it attacks enemy and then waits cooldown time
    /// </summary>
    void UpdateAttackAndWait()
    {
        Debug.Log("Attack and Wait");
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Determines whether agent's target is within
    /// sight range and has line of sight
    /// </summary>
    /// <returns>whether agent 'sees' target</returns>
    bool CanSeeTarget()
    {
        // Shoot raycast towards target, returning whether it hit
        return (Physics2D.Raycast(transform.position, targetTransform.position - transform.position, sightRange).transform == targetTransform);
    }

    #endregion

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize agent's state
        currState = startingState;

        // if target wasn't set before launch
        if (targetTransform == null)
        {
            // attempt to find and set Player as target
            try
            {
                targetTransform = GameObject.Find("Player").transform;
            }
            // Log failed attempt to find Player
            catch
            {
                Debug.LogWarning("Error: " + name + " at position " + transform.position +
                    " could not find target");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // call state-appropriate Update behavior
        switch (currState)
        {
            case ChaseStates.Idle:
                UpdateIdle();
                break;
            case ChaseStates.Pursue:
                UpdatePursue();
                break;
            case ChaseStates.AttackAndWait:
                UpdateAttackAndWait();
                break;
            default:
                break;
        }
    }

    #endregion

}
