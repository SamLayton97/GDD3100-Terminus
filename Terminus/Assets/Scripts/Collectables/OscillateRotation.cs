using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Oscillates object's rotation within a given arc
/// </summary>
public class OscillateRotation : MonoBehaviour
{
    // configuration variables
    [Range(0f, 359f)]
    [SerializeField] float oscillationArc = 30f;
    [Range(0f, 200f)]
    [SerializeField] float rotationRate = 1f;       // rate (degrees/second) by which object rotates

    // support variables
    bool rotateUp;
    float halfArc;
    float initialRotation;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // initialize oscillator
        initialRotation = transform.rotation.eulerAngles.z;
        halfArc = 0.5f * oscillationArc;
        rotateUp = Random.Range(0f, 1f) > 0.5f;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // rotate object incrementally, reversing direction when appropriate
        transform.Rotate(0, 0, rotationRate * Time.deltaTime * (rotateUp ? 1 : -1));
        if (transform.rotation.eulerAngles.z > initialRotation + halfArc || 
            transform.rotation.eulerAngles.z < initialRotation - halfArc)
            rotateUp = !rotateUp;
    }
}
