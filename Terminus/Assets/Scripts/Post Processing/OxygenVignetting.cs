﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Reflects player's oxygen status using post-processing vignetting
/// </summary>
public class OxygenVignetting : PostProcessEffectController
{
    // vignetting configuration variables
    [Range(0.01f, 5f)]
    [SerializeField] float restorationFlashRate = 1f;       // time (seconds) it takes for O2 restoration vignette to flash
    [SerializeField] Color restorationColor;                // color of vignette when player's oxygen is restored (typically blue)

    // support variables
    IEnumerator restore;
    bool restoring = false;

    /// <summary>
    /// Called before first frame Update
    /// </summary>
    void Start()
    {
        // TODO: add self as listener to relevant events
        EventManager.AddUpdateO2Listener(ScaleSuffocationVignette);
        EventManager.AddRefillO2Listener(HandleOxygenRestored);
    }

    /// <summary>
    /// Increases weight of black vignette as players continue to lose oxygen
    /// </summary>
    /// <param name="remainingOxygen">oxygen left in player's tank</param>
    void ScaleSuffocationVignette(float remainingOxygen)
    {
        // do not conflict with restoration vignette
        if (!restoring)
        {
            
        }
    }

    /// <summary>
    /// Flashes bluish vignette when player's oxygen is restored
    /// </summary>
    /// <param name="amountRestored">Amount of oxygen retored (discarded).
    /// Only needed to listen for RefillPlayerO2 event.</param>
    void HandleOxygenRestored(float amountRestored)
    {
        // start oxygen vignette coroutine
        restore = FlashOxygenVignette(1f);
        StartCoroutine(restore);
    }

    /// <summary>
    /// Flashes blue vignette over screen, 
    /// indicating player's oxygen has been restored.
    /// </summary>
    /// <param name="flashTime">time to complete flash</param>
    /// <returns></returns>
    IEnumerator FlashOxygenVignette(float flashTime)
    {
        // initialize post-process volume
        restoring = true;
        myVolume.weight = 0;

        bool increaseWeight = true;
        do
        {
            // increment/decrement weight of volume, reversing direction at apex
            myVolume.weight += Time.deltaTime * (2f / flashTime) * (increaseWeight ? 1 : -1);
            if (myVolume.weight >= 1)
                increaseWeight = !increaseWeight;

            yield return new WaitForEndOfFrame();
        } while (myVolume.weight > 0);

        // TODO: uninitialize weight
        restoring = false;

    }

}
