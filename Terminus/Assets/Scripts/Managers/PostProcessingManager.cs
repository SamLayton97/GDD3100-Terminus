using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Singleton object controlling post-processing effects
/// throughout entire application.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessingManager : MonoBehaviour
{
    // singleton variables
    static PostProcessingManager instance;

    // post-processing support
    PostProcessVolume myVolume;

    /// <summary>
    /// Read-access property returning instance of 
    /// post-processing manager singleton
    /// </summary>
    public static PostProcessingManager Instance
    {
        get { return instance; }
    }

    #region Unity Methods

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

        // otherwise, set this object as instance of singleton
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called once before first frame Update()
    /// </summary>
    void Start()
    {
        // set-up volume
        myVolume = GetComponent<PostProcessVolume>();
        myVolume.isGlobal = true;
    }

    #endregion

}
