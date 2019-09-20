using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Graphically displays player's remaining oxygen by scaling a meter
/// </summary>
public class O2Meter : MonoBehaviour
{
    // public variables
    public Image guageMeter;       // UI image to represent player's remaining O2

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if not set prior to launch, retrieve O2 meter image from child
        if (guageMeter == null)
            guageMeter = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
