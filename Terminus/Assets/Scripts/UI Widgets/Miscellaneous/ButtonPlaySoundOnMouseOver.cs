﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Plays a specified sound when user mouses over a UI button.
/// </summary>
[RequireComponent(typeof(EventTrigger))]
public class ButtonPlaySoundOnMouseOver : MonoBehaviour
{
    // public variables
    public AudioClipNames myMouseOverSound =    // sound played when user mouses over object
        AudioClipNames.UI_buttonHighlight;

    // private variables
    Button myButton;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve button component
        myButton = GetComponent<Button>();
    }

    /// <summary>
    /// Play sound when user mouses over object
    /// </summary>
    public void HandlePointerEnter()
    {
        // if interactable, play sound
        if (myButton && myButton.interactable)
            AudioManager.Play(myMouseOverSound, true);
    }
}
