using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class used to scale UI meters according
/// to a value from 0-100.
/// Note: By default, this should be applied to the image
/// itself that will be scaled.
/// </summary>
public abstract class MeterScaler : MonoBehaviour
{
    // protected variables
    [SerializeField] RectTransform myRectTransform;     // scalable RectTransform component of meter

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected void Awake()
    {
        // if not set prior to launch, retrieve meter's rect transform component
        if (myRectTransform == null)
            myRectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Used for Event Management
    /// </summary>
    protected abstract void Start();

    /// <summary>
    /// Scales meter to passed-in value
    /// </summary>
    /// <param name="newValue">new value from 0 - 100 to scale to</param>
    protected void UpdateDisplay(float newValue)
    {
        myRectTransform.localScale = new Vector2(Mathf.Clamp01(newValue / 100), myRectTransform.localScale.y);
    }
}
