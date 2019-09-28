using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio source to play all in-game sound effects
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class GameAudioSource : MonoBehaviour
{
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // keep one game audio source object for entire game
        if (!AudioManager.Initialized)
        {
            // initialize audio source and make it persist across scenes
            AudioSource audioSource = GetComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
            // destroy self if duplicate
            Destroy(gameObject);
    }
}
