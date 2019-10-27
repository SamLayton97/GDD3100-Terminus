using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Reflects player's 'sanity'/physical distortion using 
/// post-processing effects.
/// </summary>
public class DistortionEffects : PostProcessEffectController
{
    /// <summary>
    /// Called once before first frame Update()
    /// </summary>
    void Start()
    {
        myVolumes[0].weight = 1;
    }
}
