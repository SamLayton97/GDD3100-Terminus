using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Reflects player's oxygen status using post-processing vignetting
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public class OxygenVignetting : PostProcessEffectController
{
    // vignetting support variables
    [Range(0.01f, 5f)]
    [SerializeField] float restorationFlashRate = 1f;       // time (seconds) it takes for O2 restoration vignette to flash
    bool restoreCRRunning = false;

    /// <summary>
    /// Called before first frame Update
    /// </summary>
    void Start()
    {
        // add self as listener to relevant events
        EventManager.AddRefillO2Listener(HandleOxygenRestored);
    }

    /// <summary>
    /// Flashes bluish vignette when player's oxygen is restored
    /// </summary>
    /// <param name="amountRestored">Amount of oxygen retored (discarded).
    /// Only needed to listen for RefillPlayerO2 event.</param>
    void HandleOxygenRestored(float amountRestored)
    {
        // if not already running, start oxygen vignette coroutine
        if (!restoreCRRunning)
        {
            IEnumerator O2Restore = FlashOxygenVignette(1f);
            StartCoroutine(O2Restore);
        }
    }

    /// <summary>
    /// Flashes blue vignette over screen, 
    /// indicating player's oxygen has been restored.
    /// </summary>
    /// <param name="flashTime">time to complete flash</param>
    /// <returns></returns>
    IEnumerator FlashOxygenVignette(float flashTime)
    {
        restoreCRRunning = true;
        bool increaseWeight = true;
        do
        {
            // increment/decrement weight of volume, reversing direction at apex
            myVolume.weight += Time.deltaTime * (2f / flashTime) * (increaseWeight ? 1 : -1);
            if (myVolume.weight >= 1)
                increaseWeight = !increaseWeight;

            yield return new WaitForEndOfFrame();
        } while (myVolume.weight > 0);
        restoreCRRunning = false;
    }

}
