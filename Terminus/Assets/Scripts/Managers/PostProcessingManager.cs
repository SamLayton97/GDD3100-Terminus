using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Singleton object controlling post-processing effects
/// throughout entire application.
/// </summary>
public class PostProcessingManager : MonoBehaviour
{
    // singleton variables
    static PostProcessingManager instance;

    /// <summary>
    /// Read-access property returning instance of 
    /// post-processing manager singleton
    /// </summary>
    public static PostProcessingManager Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if singleton has already initialized as another instance
        if (instance != null && Instance != this)
        {
            // destroy this instance and break
            Destroy(gameObject);
            return;
        }

        // set this object as instance of singleton
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
