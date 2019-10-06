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
    // serialized UI variables
    [SerializeField] Image controlsDiagramImage;            // diagram displaying control scheme player has selected

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // TODO: initialize diagram to reflect currently selected control scheme
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
    }

    #region Public Methods

    /// <summary>
    /// Swaps player's control scheme to standard
    /// </summary>
    public void SwapToStandard()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.standard);
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scheme to specialist
    /// </summary>
    public void SwapToSpecialist()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.specialist);
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scene to left-handed
    /// </summary>
    public void SwapToLeftHanded()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.leftHanded);
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scheme to lefty-specialist
    /// </summary>
    public void SwapToLeftySpecialist()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.leftySpecialist);
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    #endregion

}
