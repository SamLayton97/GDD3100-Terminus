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
    public RectTransform guageMeter;    // RectTransform component of meter representing player's remaining O2

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if not set prior to launch, retrieve rect transform component
        if (guageMeter == null)
            guageMeter = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    void Start()
    {
        // add self as listener to Update O2 display event
        EventManager.AddUpdateO2Listener(UpdateO2Display);
    }

    /// <summary>
    /// Updates O2 meter by scaling it
    /// </summary>
    /// <param name="remainingOxygen">percentage of oxygen left in player's tank</param>
    void UpdateO2Display(float remainingOxygen)
    {
        guageMeter.localScale = new Vector2(Mathf.Clamp01(remainingOxygen / 100), guageMeter.localScale.y);
    }
}
