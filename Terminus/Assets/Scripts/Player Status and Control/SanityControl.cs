using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages sanity depletion and re-gain of agent, 
/// including (TODO) hallucinations and player death
/// </summary>
[RequireComponent(typeof(OxygenControl))]
public class SanityControl : MonoBehaviour
{
    // public variables
    float lowOxygenThreshold = 40f;         // threshold on which player starts losing sanity due to low oxygen

    // private variables
    OxygenControl myOxygenControl;          // reference to player's oxygen control (sanity depletes when below O2 threshold)

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myOxygenControl = GetComponent<OxygenControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
