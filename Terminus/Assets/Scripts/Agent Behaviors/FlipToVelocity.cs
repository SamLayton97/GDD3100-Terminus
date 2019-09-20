using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flips agent along y-axis to face direction of current velocity
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class FlipToVelocity : MonoBehaviour
{
    // public variables
    public bool facingLeftByDefault = true;     // flag determining initial direction agent's sprite faces

    // private variables
    Rigidbody2D myRigidbody2D;                  // agent's rigidbody 2d component (used to find its velocity)

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // calculate dot product of agent's velocity relative to horizontal
        float velDotProduct = Vector2.Dot(myRigidbody2D.velocity, Vector2.right);
        Debug.Log(velDotProduct);
    }
}
