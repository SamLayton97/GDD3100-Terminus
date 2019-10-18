using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes aspects of game relevant to user's display.
/// This happens once when game loads.
/// </summary>
public class GameDisplayInitializer : MonoBehaviour
{
    // support variables
    bool initialized = false;
    int targetFrameRate = 60;           // max framerate game will run at (default to 60 fps)

    // singleton objects
    [SerializeField]
    List<GameObject> displaySingletons =    // list of display-controlling singletons to spawn/initialize on application start
        new List<GameObject>();     

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

        // instantiate each display singleton
        foreach (GameObject singleton in displaySingletons)
            Instantiate(singleton);

    }

    void Update()
    {
        //Debug.Log(Application.targetFrameRate + " " + 1f / Time.deltaTime);
    }
}
