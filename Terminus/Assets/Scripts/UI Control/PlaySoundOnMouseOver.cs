using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Plays a specified sound when user mouses over a UI element.
/// Typically used by interactable elements such as buttons.
/// </summary>
[RequireComponent(typeof(EventTrigger))]
public class PlaySoundOnMouseOver : MonoBehaviour
{
    // public variables
    public AudioClipNames myMouseOverSound =    // sound played when user mouses over object
        AudioClipNames.UI_buttonHighlight;

    /// <summary>
    /// Play sound when user mouses over object
    /// </summary>
    public void HandlePointerEnter()
    {
        AudioManager.Play(myMouseOverSound, true);
    }
}
