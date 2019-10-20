using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomizes initial rotation of an object around the z-axis
/// </summary>
public class RandomizeInitialRotation : MonoBehaviour
{
    // public variables
    public float rotationLowerBound = 0f;       // inclusive lower bound of random rotation range
    public float rotationUpperBound = 360f;     // inclusive upper bound of random rotation range

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // clamp upper and lower bounds
        rotationLowerBound = Mathf.Clamp(rotationLowerBound, -360f, rotationUpperBound - 1);
        rotationUpperBound = Mathf.Clamp(rotationUpperBound, rotationLowerBound + 1, 360f);

        // rotate object about the z-axis
        transform.Rotate(new Vector3(0, 0, Random.Range(rotationLowerBound, rotationUpperBound)));
    }
}
