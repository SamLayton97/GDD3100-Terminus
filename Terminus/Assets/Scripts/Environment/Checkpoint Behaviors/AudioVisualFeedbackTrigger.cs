using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides general audio-visual feedback 
/// to player when they collide with checkpoint.
/// </summary>
public class AudioVisualFeedbackTrigger : MonoBehaviour
{
    // audio feedback variables
    [SerializeField] AudioClipNames triggerSound        // sound played when user enters trigger
        = AudioClipNames.atm_piano;

    /// <summary>
    /// Called when player enters trigger box
    /// </summary>
    /// <param name="other">collision info</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if player's body entered trigger
        if (!other.isTrigger)
        {
            // play sound
            AudioManager.Play(triggerSound, true);
        }
    }
}
