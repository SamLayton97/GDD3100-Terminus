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
    bool initialized = false;

    // serialized fields
    int targetFrameRate = 60;           // max framerate game will run at (default to 60 fps)

    /// <summary>
    /// Used for early display initialization
    /// </summary>
    void Awake()
    {
        // never initialize twice
        if (!initialized)
        {
            initialized = true;

            // lock framerate
            Application.targetFrameRate = targetFrameRate;
        }
    }

    void Update()
    {
        Debug.Log(Application.targetFrameRate + " " + 1f / Time.deltaTime);
    }
}
