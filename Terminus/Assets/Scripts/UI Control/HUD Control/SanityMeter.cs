using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphically displays player's sanity by scaling a meter
/// </summary>
public class SanityMeter : MeterScaler
{
    /// <summary>
    /// Called before the first frame of Update
    /// </summary>
    protected override void Start()
    {
        // add self as listener to update sanity display event
        EventManager.AddUpdateSanityListener(UpdateDisplay);
    }
}
