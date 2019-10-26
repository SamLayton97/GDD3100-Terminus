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
    [SerializeField] Text controlsDiagramName;              // name of control scheme currently selected
    [SerializeField] ToggleGroup controlsToggleGroup;       // group of toggles allowing user to select control scheme

    // support variables
    List<Toggle> controlToggles = new List<Toggle>();

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize diagram
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


    public void SelectControlScheme(bool schemeSelected)
    {
        controlsToggleGroup.SetAllTogglesOff();
    }

    #endregion

}
