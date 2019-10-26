using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        // set up volumes, adding required component if not added
        if (myVolumes.Count < 1)
            myVolumes.Add(GetComponent<PostProcessVolume>());
        foreach (PostProcessVolume volume in myVolumes)
            volume.isGlobal = true;
    }
}
