using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes game's background data once once
/// when application loads.
/// </summary>
public class GameDataInitializer : MonoBehaviour
{
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize data
        CraftableItemsRegistry.Initialize();
    }
}
