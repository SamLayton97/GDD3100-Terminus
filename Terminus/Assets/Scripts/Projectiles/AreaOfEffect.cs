﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls area-of-effect done to enemies. Typically used by 
/// explosions created by projectiles who burst on contact/over time.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class AreaOfEffect : MonoBehaviour
{
    // serialized fields
    [SerializeField] float damage = 0f;             // raw damage dealt to enemy agents inside area of effect
    [SerializeField] float poisonDamage = 0f;       // damage dealt to enemy agent over time (adds poison attribute to agent if over 0)

    // private variables
    CircleCollider2D myTriggerCollider;             // trigger used to determine what objects to apply effect to

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
    /// Called when another object enter this object's
    /// trigger collider.
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // if object in collision is an enemy agent
        if (collision.gameObject.layer == LayerMask.NameToLayer("Alien"))
        {
            Debug.Log("poison enemy");
        }
    }

    /// <summary>
    /// Destroys explosion when it's animation ends
    /// </summary>
    public void DestroyOnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
