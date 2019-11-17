using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Pulses opacity of a canvas group over timescale-dependent time
/// </summary>
public class PulseOpacity : MonoBehaviour
{
    // pulse configuration variables
    [SerializeField] CanvasGroup pulseControl;      // canvas group controlling alpha value
    [Range(0f, 1f)]
    [SerializeField] float pulseValley = 0f;        // lower bounds of pulse range
    [Range(0f, 1f)]
    [SerializeField] float pulsePeak = 0f;          // upper bounds of pulse range
    [Range(0f, 3f)]
    [SerializeField] float pulseRate = 1f;          // rate at which object shifts from peak to valley/vice versa

    // pulse support variables
    bool ascending = true;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if not set prior to setup
        if (!pulseControl)
            // assume pulse controller is part of this object
            pulseControl = GetComponent<CanvasGroup>();

        // set starting opacity
        pulseControl.alpha = pulseValley;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // adjust alpha of pulse control
        pulseControl.alpha = Mathf.Clamp(pulseControl.alpha + Time.deltaTime * pulseRate * (ascending ? 1 : -1), 
            pulseValley, pulsePeak);

        // reverse direction at peak/valley
        if (pulseControl.alpha >= pulsePeak || pulseControl.alpha <= pulseValley)
            ascending = !ascending;
    }

}
