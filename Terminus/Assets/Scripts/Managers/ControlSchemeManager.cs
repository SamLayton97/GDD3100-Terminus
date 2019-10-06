using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enumeration of keyboard control schemes
/// </summary>
public enum ControlSchemes
{
    standard,
    specialist,
    leftHanded,
    leftySpecialist
}

/// <summary>
/// Manages setting of inputs according to selected
/// keyboard and mouse layout
/// </summary>
public static class ControlSchemeManager
{
    static ControlSchemes currScheme = ControlSchemes.standard;     // control scheme currently used by player

    /// <summary>
    /// Read access property returning player's 
    /// currently selected control scheme
    /// </summary>
    public static ControlSchemes CurrentControlScheme
    {
        get { return CurrentControlScheme; }
    }

    /// <summary>
    /// Initializes control scheme manager, setting default scheme
    /// and loading in layout diagrams
    /// </summary>
    public static void Initialize()
    {

    }

    /// <summary>
    /// Sets control scheme used by player to one given, modifying
    /// inputs and updating layout to display.
    /// </summary>
    /// <param name="newScheme">new scheme used by player</param>
    public static void SetControlScheme(ControlSchemes newScheme)
    {
        Debug.Log(newScheme);
    }
}
