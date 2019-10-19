using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages toggling various display options
/// from the options menu.
/// </summary>

public class DisplayOptionsSelector : MonoBehaviour
{
    // selection support
    [SerializeField] Toggle toggleCCButton;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // initialize display options button to reflect current configuration
        if (ClosedCaptions.Instance.ccEnabled)
            toggleCCButton.isOn = true;
    }

    /// <summary>
    /// Toggles closed captions when player clicks appropriate button
    /// </summary>
    /// <param name="ccEnabled">new cc enabled state</param>
    public void ToggleCCOnClick(bool ccEnabled)
    {
        ClosedCaptions.Instance.ccEnabled = ccEnabled;
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }
}
