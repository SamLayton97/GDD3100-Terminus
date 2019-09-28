using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a specified sound when user mouses over object.
/// Typically used by interactable UI elements such as buttons.
/// </summary>
public class PlaySoundOnMouseOver : MonoBehaviour
{
    // public variables
    public AudioClipNames myMouseOverSound =    // sound played when user mouses over object
        AudioClipNames.UI_buttonHighlight;
}
