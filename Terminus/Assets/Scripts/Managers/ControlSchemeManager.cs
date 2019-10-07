using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enumeration of keyboard control schemes
/// </summary>
public enum ControlSchemes
{
    Standard,
    Specialist,
    LeftHanded,
    LeftySpecialist
}

/// <summary>
/// Manages setting of inputs according to selected
/// keyboard and mouse layout
/// </summary>
public static class ControlSchemeManager
{
    // private variables
    static bool initialized = false;
    static ControlSchemes currScheme = ControlSchemes.Standard;     // control scheme currently used by player
    static Dictionary<ControlSchemes, Sprite> controlLayouts =      // dictionary pairing control schemes with layout diagrams
        new Dictionary<ControlSchemes, Sprite>();

    #region Properties

    /// <summary>
    /// Read access property returning player's 
    /// currently selected control scheme
    /// </summary>
    public static ControlSchemes CurrentControlScheme
    {
        get { return currScheme; }
    }

    /// <summary>
    /// Read access property returning diagram of
    /// control scheme currently used by player
    /// </summary>
    public static Sprite CurrentSchemeDiagram
    {
        get { return controlLayouts[currScheme]; }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Initializes control scheme manager, setting default scheme
    /// and loading in layout diagrams
    /// </summary>
    public static void Initialize()
    {
        // never initialize twice
        if (!initialized)
        {
            initialized = true;

            // initialize player's control scheme to default
            currScheme = ControlSchemes.Standard;

            // load in control scheme diagrams from Resources/ControlSchemes
            controlLayouts.Add(ControlSchemes.Standard, Resources.Load<Sprite>("ControlSchemes/spr_standardControls"));
            controlLayouts.Add(ControlSchemes.Specialist, Resources.Load<Sprite>("ControlSchemes/spr_specialistControls"));
            controlLayouts.Add(ControlSchemes.LeftHanded, Resources.Load<Sprite>("ControlSchemes/spr_leftyControls"));
            controlLayouts.Add(ControlSchemes.LeftySpecialist, Resources.Load<Sprite>("ControlSchemes/spr_leftySpecialistControls"));
        }
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
        // update current scheme
        currScheme = newScheme;

        // according to new scheme adjust input axes
        switch (newScheme) 
        {
            case ControlSchemes.Standard:
                // set mouse buttons for right hand
                CustomInputManager.SetMouseButtonMap("Fire", 0);
                CustomInputManager.SetMouseButtonMap("ShowHideCraftingMenu", 1);

                break;
            case ControlSchemes.Specialist:
                // set mouse buttons for right hand
                CustomInputManager.SetMouseButtonMap("Fire", 0);
                CustomInputManager.SetMouseButtonMap("ShowHideCraftingMenu", 1);

                // set weapon select key mappings
                CustomInputManager.SetKeyMap("SelectWeapon1", KeyCode.A);
                CustomInputManager.SetKeyMap("SelectWeapon2", KeyCode.S);
                CustomInputManager.SetKeyMap("SelectWeapon3", KeyCode.D);
                CustomInputManager.SetKeyMap("SelectWeapon4", KeyCode.F);

                break;
            case ControlSchemes.LeftHanded:
                // set mouse buttons for left hand
                CustomInputManager.SetMouseButtonMap("Fire", 1);
                CustomInputManager.SetMouseButtonMap("ShowHideCraftingMenu", 0);

                break;
            case ControlSchemes.LeftySpecialist:
                // set mouse buttons for left hand
                CustomInputManager.SetMouseButtonMap("Fire", 1);
                CustomInputManager.SetMouseButtonMap("ShowHideCraftingMenu", 0);

                // set weapon select key mappings
                CustomInputManager.SetKeyMap("SelectWeapon1", KeyCode.H);
                CustomInputManager.SetKeyMap("SelectWeapon2", KeyCode.J);
                CustomInputManager.SetKeyMap("SelectWeapon3", KeyCode.K);
                CustomInputManager.SetKeyMap("SelectWeapon4", KeyCode.L);

                break;
            // if default case occurs, simply map inputs to standard scheme
            default:
                Debug.Log("Warning: Attempted to set controls to unknown configuration. Using standard control scheme.");
                currScheme = ControlSchemes.Standard;

                // set mouse buttons for right hand
                CustomInputManager.SetMouseButtonMap("Fire", 0);
                CustomInputManager.SetMouseButtonMap("ShowHideCraftingMenu", 1);

                break;
        }
    }

    #endregion

}
