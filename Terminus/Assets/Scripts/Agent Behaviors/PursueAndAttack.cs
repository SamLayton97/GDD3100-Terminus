using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy agent behavior of pursuing player once within
/// sight range and attacking within melee distance.
/// </summary>
public class PursueAndAttack : MonoBehaviour
{
    // public variables
    public ChaserStates startingState = ChaserStates.Idle;  // state which pursuing agent starts in (typically Idle)
    public Transform targetTransform;                       // transform of target to pursue (typically player)

    // private variables
    ChaserStates currState;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if target wasn't set before launch
        if (targetTransform == null)
        {
            // attempt to find and set Player as target
            try
            {
                targetTransform = GameObject.Find("Player").transform;
            }
            // Log failed attempt to find Player
            catch
            {
                Debug.LogWarning("Error: " + name + " at position " + transform.position +
                    " could not find target");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
