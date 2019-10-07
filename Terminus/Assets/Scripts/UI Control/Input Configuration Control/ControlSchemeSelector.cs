﻿using System.Collections;
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
    [SerializeField] Text controlsDiagramName;              // name of control scheme currently selected

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize diagram to reflect currently selected control scheme
        UpdateDiagram();
    }

    /// <summary>
    /// Updates diagram according to control scheme selected by user
    /// </summary>
    void UpdateDiagram()
    {
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
        controlsDiagramName.text = (ControlSchemeManager.CurrentScheme).ToString();
    }

    #region Public Methods

    /// <summary>
    /// Swaps player's control scheme to standard
    /// </summary>
    public void SwapToStandard()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.Standard);
        UpdateDiagram();
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scheme to specialist
    /// </summary>
    public void SwapToSpecialist()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.Specialist);
        UpdateDiagram();
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scene to left-handed
    /// </summary>
    public void SwapToLeftHanded()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.LeftHanded);
        UpdateDiagram();
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Swaps player's control scheme to lefty-specialist
    /// </summary>
    public void SwapToLeftySpecialist()
    {
        // update control scheme and play select sound effect
        ControlSchemeManager.SetControlScheme(ControlSchemes.LeftySpecialist);
        UpdateDiagram();
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    #endregion

}