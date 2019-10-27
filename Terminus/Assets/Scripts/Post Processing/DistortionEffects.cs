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
    // post processing configuration variables
    [Range(0f, 100f)]
    [SerializeField] float startHallucinating = 40f;        // point at which persistent hallucination effect begins playing

    /// <summary>
    /// Called before first frame of Update()
    /// </summary>
    void Start()
    {
        // add self as listener to appropriate events
        EventManager.AddUpdateSanityListener(ScalePersistentEffect);
    }

    /// <summary>
    /// Increases weight of persistent hallucination 
    /// effect as players continue to lose sanity
    /// </summary>
    /// <param name="remainingSanity">player's remaining sanity</param>
    void ScalePersistentEffect(float remainingSanity)
    {
        myVolumes[0].weight = Mathf.Max(0, (1 - (remainingSanity / startHallucinating)));
    }
}
