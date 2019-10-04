using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Audio source to play all in-game sound effects excluding music
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class GameAudioSource : MonoBehaviour
{
    // public variables
    public AudioSource soundEffectsAudioSource;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // keep one game audio source object for entire game
        if (!AudioManager.Initialized)
        {
            // initialize audio source and make it persist across scenes
            AudioManager.Initialize(soundEffectsAudioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
            // destroy self if duplicate
            Destroy(gameObject);
    }
}
