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
    [SerializeField] ToggleGroup controlsToggleGroup;       // group of toggles allowing user to select control scheme

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // initialize diagram and toggle group with current control scheme
        controlsToggleGroup.transform.GetChild((int)ControlSchemeManager.CurrentScheme).GetComponent<Toggle>().isOn = true;
        UpdateDiagram();
    }

    /// <summary>
    /// Updates diagram according to control scheme selected by user
    /// </summary>
    void UpdateDiagram()
    {
        controlsDiagramImage.sprite = ControlSchemeManager.CurrentSchemeDiagram;
    }

    #region Public Methods

    /// <summary>
    /// Sets control scheme when user interacts with a toggle
    /// </summary>
    /// <param name="newScheme">bounded index of selected control scheme
    /// within ControlSchemes enum</param>
    public void SelectControlScheme(int newScheme)
    {
        // update control scheme and diagram
        ControlSchemeManager.SetControlScheme((ControlSchemes)Mathf.Max(0, 
            Mathf.Min(System.Enum.GetNames(typeof(ControlSchemes)).Length, newScheme)));
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
        UpdateDiagram();
    }

    #endregion

}
