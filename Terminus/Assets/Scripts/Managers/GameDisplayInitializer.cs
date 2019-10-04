using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes aspects of game relevant to user's display.
/// This happens once when game loads.
/// </summary>
public class GameDisplayInitializer : MonoBehaviour
{
    // private variables
    bool isInitialized = false;

    // serialized fields
    [Range(30, 300)]
    [SerializeField]
    int targetFrameRate = 60;           // max framerate game will run at

    /// <summary>
    /// Used for early display initialization
    /// </summary>
    void Awake()
    {
        // TODO: lock framerate

    }

    void Update()
    {
        Debug.Log("Target: " + Application.targetFrameRate + " Actual: " + (1.0f / Time.deltaTime));
    }
}
