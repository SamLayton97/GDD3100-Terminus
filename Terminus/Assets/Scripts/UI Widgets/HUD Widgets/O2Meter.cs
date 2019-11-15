using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphically displays player's remaining oxygen by scaling a meter
/// </summary>
public class O2Meter : MeterScaler
{
    // lazy fill support variables
    [SerializeField] RectTransform lazyFill;
    [Range(0f, 3f)]
    [SerializeField] float lazyFillWait = 1f;           // time 'lazy' oxygen meter fill waits before shrinking
    [Range(0f, 10f)]
    [SerializeField] float lazyFillShrinkRate = 1f;     // rate at which meter shrinks
    IEnumerator lazyFillCoroutine;                      // coroutine controlling oxygen meter's lazy fill

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    protected override void Start()
    {
        // add self as listener to Update O2 display event
        EventManager.AddUpdateO2Listener(UpdateDisplay);
    }

    /// <summary>
    /// Scales oxygen meter to passed in value,
    /// initializing meter's 'lazy' fill.
    /// </summary>
    /// <param name="newValue">new value from 0 - 100 to scale to</param>
    protected override void UpdateDisplay(float newValue)
    {
        // start coroutine to shrink lazy fill
        lazyFillCoroutine = ShrinkLazyFill();
        StartCoroutine(lazyFillCoroutine);

        // scale meter
        base.UpdateDisplay(newValue);
    }

    /// <summary>
    /// Shrinks oxygen meter's lazy fill to match 
    /// scale of actual meter after waiting a period.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShrinkLazyFill()
    {
        // lock lazy fill's scale for wait duration
        Debug.Log("lock");
        yield return new WaitForSeconds(lazyFillWait);
        Debug.Log("unlock");
    }
}
