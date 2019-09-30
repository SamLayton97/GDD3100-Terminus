using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio source to play all music
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicAudioSource : MonoBehaviour
{
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // keep one game audio source object for entire game
        if (!MusicManager.Initialized)
        {
            // initialize audio source and make it persist across scenes
            AudioSource audioSource = GetComponent<AudioSource>();
            MusicManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
            // destroy self if duplicate
            Destroy(gameObject);
    }
}
