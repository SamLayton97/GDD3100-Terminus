﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates an object to face velocity it's traveling in
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class FaceVelocity : MonoBehaviour
{
    // public variables
    public bool continuous = false;             // determines whether object should continue to rotate towards its velocity every frame
    public Rigidbody2D relativeTo = null;       // rigidbody of object to calculate relative velocity to (if null, velocity is relative to world)

    // private variables
    Rigidbody2D myRigidbody2D;                  // rigidbody component used to find object's current velocity

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // if rotation shouldn't be continuous, rotate once
        if (!continuous)
        {
            Vector2 relativeVelocity = myRigidbody2D.velocity.normalized;
            if (relativeTo != null)
                relativeVelocity = relativeVelocity - relativeTo.velocity.normalized;

            Debug.Log(relativeVelocity);
            transform.Rotate(new Vector3(0, 0, Mathf.Atan2(relativeVelocity.y, relativeVelocity.x) * Mathf.Rad2Deg));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
