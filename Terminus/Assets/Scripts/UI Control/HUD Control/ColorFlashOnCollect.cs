using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes appropriate HUD element to flash between two
/// colors when player collects a new weapon.
/// </summary>
[RequireComponent(typeof(Image))]
public class ColorFlashOnCollect : MonoBehaviour
{
    // configuration variables
    [SerializeField] Color toColor;             // color to flash to
    [SerializeField] float flashTime = 1f;      // time (seconds) to flash from one color to another

    // support variables
    Image flashImage;
    Color fromColor;
    IEnumerator flashCoroutine;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components/information
        flashImage = GetComponent<Image>();
        fromColor = flashImage.color;
    }

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // TODO: add self as listener to add weapon event

    }

    


}
