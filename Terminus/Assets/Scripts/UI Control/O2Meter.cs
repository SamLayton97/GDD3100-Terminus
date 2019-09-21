using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphically displays player's remaining oxygen by scaling a meter
/// </summary>
public class O2Meter : MeterScaler
{
    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    protected override void Start()
    {
        // add self as listener to Update O2 display event
        EventManager.AddUpdateO2Listener(UpdateDisplay);
    }
}
