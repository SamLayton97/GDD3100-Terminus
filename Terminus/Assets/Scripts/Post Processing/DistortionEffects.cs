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

    // support variables
    float sanityLastFrame = 100f;

    /// <summary>
    /// Called before first frame of Update()
    /// </summary>
    void Start()
    {
        // add self as listener to appropriate events
        EventManager.AddUpdateSanityListener(ScaleHallucinationEffect);
        EventManager.AddUpdateSanityListener(ScaleDistortionEffect);
    }

    /// <summary>
    /// Increases weight of persistent hallucination 
    /// effect as players continue to lose sanity
    /// </summary>
    /// <param name="remainingSanity">player's remaining sanity</param>
    void ScaleHallucinationEffect(float remainingSanity)
    {
        myVolumes[0].weight = Mathf.Max(0, (1 - (remainingSanity / startHallucinating)));
    }

    /// <summary>
    /// Increases weight/volume of effects tied to 
    /// negative change in player's sanity
    /// </summary>
    /// <param name="remainingSanity"></param>
    void ScaleDistortionEffect(float remainingSanity)
    {
        Debug.Log("Delta: " + ((remainingSanity - sanityLastFrame) / Time.deltaTime));

        // store sanity to calculate delta for next frame
        sanityLastFrame = remainingSanity;
    }
}
