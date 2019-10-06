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
    // private variables
    static ControlSchemes currScheme = ControlSchemes.standard;     // control scheme currently used by player
    static Dictionary<ControlSchemes, Sprite> controlLayouts =      // dictionary pairing control schemes with layout diagrams
        new Dictionary<ControlSchemes, Sprite>();

    #region Properties

    /// <summary>
    /// Read access property returning player's 
    /// currently selected control scheme
    /// </summary>
    public static ControlSchemes CurrentControlScheme
    {
        get { return CurrentControlScheme; }
    }

    /// <summary>
    /// Read access property returning diagram of
    /// control scheme currently used by player
    /// </summary>
    public static Sprite CurrentSchemeDiagram
    {
        get { return GetControlSchemeDiagram(currScheme); }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Initializes control scheme manager, setting default scheme
    /// and loading in layout diagrams
    /// </summary>
    public static void Initialize()
    {
        // initialize player's control scheme to default
        currScheme = ControlSchemes.standard;

        // load in control scheme diagrams from Resources/ControlSchemes
        controlLayouts.Add(ControlSchemes.standard, Resources.Load<Sprite>("ControlSchemes/spr_standardControls"));
        controlLayouts.Add(ControlSchemes.specialist, Resources.Load<Sprite>("ControlSchemes/spr_specialistControls"));
        controlLayouts.Add(ControlSchemes.leftHanded, Resources.Load<Sprite>("ControlSchemes/spr_leftyControls"));
        controlLayouts.Add(ControlSchemes.leftySpecialist, Resources.Load<Sprite>("ControlSchemes/spr_leftySpecialistControls"));
    }

    /// <summary>
    /// Retrieves diagram corresponding to given control scheme
    /// </summary>
    /// <param name="scheme">scheme to retrieve</param>
    public static Sprite GetControlSchemeDiagram(ControlSchemes scheme)
    {
        return controlLayouts[scheme];
    }

    /// <summary>
    /// Sets control scheme used by player to one given, modifying
    /// inputs and updating layout to display.
    /// </summary>
    /// <param name="newScheme">new scheme used by player</param>
    public static void SetControlScheme(ControlSchemes newScheme)
    {
        // update scheme used and layout displayed
        currScheme = newScheme;
    }

    #endregion

}
