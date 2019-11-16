using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys one-shot particle effects after they've ended
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticleOnEnd : MonoBehaviour
{
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // destroy object after effect's duration
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}