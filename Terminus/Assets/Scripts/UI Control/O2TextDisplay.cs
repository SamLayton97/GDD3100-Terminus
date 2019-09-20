using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Numerically displays player's remaining oxygen percentage
/// </summary>
public class O2TextDisplay : MonoBehaviour
{
    // public variables
    public Text remainingO2Text;            // UI textbox to display player's remaining oxygen
    public string remainingO2Prefix = "";   // text to prepend onto remaining O2 %
    public string remainingO2Suffix = "";   // text to append onto remaining O2 %

    // Used for initialization
    void Awake()
    {
        // if textbox wasn't set in editor, retrieve first Text component in children
        if (remainingO2Text == null)
            remainingO2Text = GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to update O2 display event
        EventManager.AddUpdateO2Listener(UpdateO2Display);
    }

    /// <summary>
    /// Updates text display of player's remaining oxygen.
    /// </summary>
    /// <param name="remainingOxygen">amount of player's oxygen
    /// remaining in their tank</param>
    void UpdateO2Display(float remainingOxygen)
    {
        remainingO2Text.text = remainingO2Prefix + (int)remainingOxygen + remainingO2Suffix;
    }
}
