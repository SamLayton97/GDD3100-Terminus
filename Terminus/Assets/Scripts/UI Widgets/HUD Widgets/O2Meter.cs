using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphically displays player's remaining oxygen by scaling a meter
/// </summary>
public class O2Meter : MeterScaler
{
    // lazy fill configuration support
    [SerializeField] RectTransform lazyFill;
    [SerializeField] float lazyFillWait = 1f;           // time 'lazy' oxygen meter fill waits before shrinking
    [SerializeField] float lazyFillShrinkRate = 1f;     // rate at which meter shrinks

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
        // scale meter
        base.UpdateDisplay(newValue);
    }
}
