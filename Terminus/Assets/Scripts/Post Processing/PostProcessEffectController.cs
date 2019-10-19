using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Generic parent class for all post-processing effect controllers.
/// Used for initialization of post-processing volume.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public abstract class PostProcessEffectController : MonoBehaviour
{
    // volume support variables
    protected PostProcessVolume myVolume;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // set up volume
        myVolume = GetComponent<PostProcessVolume>();
        myVolume.isGlobal = true;
    }
}
