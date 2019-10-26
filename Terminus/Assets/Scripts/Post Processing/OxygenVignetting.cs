using System.Collections;
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
    [Range(0, 100)]
    [SerializeField] int startSuffocating = 40;             // remaining oxygen at which player begins suffocating

    // support variables
    IEnumerator restore;
    bool restoring = false;

    /// <summary>
    /// Called before first frame Update
    /// </summary>
    void Start()
    {
        // add self as listener to relevant events
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
        myVolumes[1].weight = Mathf.Max(0, (1 - (remainingOxygen / startSuffocating)) * (restoring ? 0 : 1));
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
        myVolumes[0].weight = 0;

        bool increaseWeight = true;
        do
        {
            // increment/decrement weight of volume, reversing direction at apex
            myVolumes[0].weight += Time.deltaTime * (2f / flashTime) * (increaseWeight ? 1 : -1);
            if (myVolumes[0].weight >= 1)
                increaseWeight = !increaseWeight;

            yield return new WaitForEndOfFrame();
        } while (myVolumes[0].weight > 0);

        // set restore flag to false
        restoring = false;

    }

}
