using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Reflects player's 'sanity'/physical distortion using 
/// post-processing effects.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class DistortionEffects : PostProcessEffectController
{
    // post processing configuration variables
    [Range(0f, 100f)]
    [SerializeField] float startHallucinating = 40f;        // point at which persistent hallucination effect begins playing
    [Range(0f, 10f)]
    [SerializeField] float distortionCeiling = 3f;          // negative change in sanity that translates to max distortion weight

    // support variables
    float sanityLastFrame = 100f;
    AudioSource myDistortSource;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant components
        myDistortSource = GetComponent<AudioSource>();
    }

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
        // scale weight and volume by negative change in sanity
        float newScale = Mathf.Lerp(myVolumes[1].weight,
            Mathf.Clamp01(sanityLastFrame - remainingSanity) / (Time.deltaTime * distortionCeiling), Time.deltaTime);
        myVolumes[1].weight = newScale;
        myDistortSource.volume = newScale;

        // store sanity to calculate delta for next frame
        sanityLastFrame = remainingSanity;
    }
}
