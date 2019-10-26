using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Generic parent class for all post-processing effect controllers.
/// Used for initialization of post-processing volume(s).
/// </summary>
[RequireComponent(typeof(PostProcessVolume))]
public abstract class PostProcessEffectController : MonoBehaviour
{
    // volume configuration variables
    [SerializeField]
    protected List<PostProcessVolume> myVolumes;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // grab post process volume if not set up beforehand
        if (myVolumes.Count < 1)
            myVolumes.Add(GetComponent<PostProcessVolume>());
        
        // add self as delegate to load scene event
        SceneManager.sceneLoaded += ResetVolumes;
    }

    /// <summary>
    /// Sets all volumes under controller to global with a weight of 0
    /// when scene is loaded. Note: Parameters are only needed to
    /// listen for sceneLoaded event.
    /// </summary>
    /// <param name="sceneLoaded">scene now loaded</param>
    /// <param name="mode">mode under which scene was loaded</param>
    protected void ResetVolumes(Scene sceneLoaded, LoadSceneMode mode)
    {
        foreach (PostProcessVolume volume in myVolumes)
        {
            volume.isGlobal = true;
            volume.weight = 0;
        }
    }
}
