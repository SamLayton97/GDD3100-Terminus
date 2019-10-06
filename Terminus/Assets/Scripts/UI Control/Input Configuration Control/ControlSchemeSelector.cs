using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An enumeration of keyboard control schemes
/// </summary>
//public enum ControlSchemes
//{
//    standard,
//    specialist,
//    leftHanded,
//    leftySpecialist
//}

/// <summary>
/// Manages swapping between control schemes from
/// the options menu.
/// </summary>
public class ControlSchemeSelector : MonoBehaviour
{
    /// <summary>
    /// Swaps player's control scheme to standard
    /// </summary>
    public void SwapToStandard()
    {
        Debug.Log("standard");
    }

    /// <summary>
    /// Swaps player's control scheme to specialist
    /// </summary>
    public void SwapToSpecialist()
    {
        Debug.Log("specialist");
    }

    /// <summary>
    /// Swaps player's control scene to left-handed
    /// </summary>
    public void SwapToLeftHanded()
    {
        Debug.Log("lefty");
    }

    /// <summary>
    /// Swaps player's control scheme to lefty-specialist
    /// </summary>
    public void SwapToLeftySpecialist()
    {
        Debug.Log("l specialist");
    }
}
