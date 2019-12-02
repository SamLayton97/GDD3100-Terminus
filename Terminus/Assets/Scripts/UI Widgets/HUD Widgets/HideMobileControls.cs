using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hides/reveals UI element when player pauses/unpauses
/// game or opens/closes crafting menu
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class HideMobileControls : HideOnPause
{
    /// <summary>
    /// Start is called before the first frame of Update()
    /// </summary>
    protected override void Start()
    {
        // TODO: add self as listener for show/hide controls event

    }
}
