using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes game's background data once once
/// when application loads.
/// </summary>
public class GameDataInitializer : MonoBehaviour
{
    // private variables
    bool initialized = false;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // never initialize twice
        if (!initialized)
        {
            initialized = true;

            // initialize craftable items registry
            CraftableItemsRegistry.Initialize();
        }
    }
}
