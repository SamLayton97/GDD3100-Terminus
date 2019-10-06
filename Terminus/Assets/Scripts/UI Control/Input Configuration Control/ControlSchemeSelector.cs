using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.standard);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scheme to specialist
    /// </summary>
    public void SwapToSpecialist()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.specialist);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scene to left-handed
    /// </summary>
    public void SwapToLeftHanded()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.leftHanded);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scheme to lefty-specialist
    /// </summary>
    public void SwapToLeftySpecialist()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.leftySpecialist);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }
}
