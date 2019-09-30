using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages playing and stopping of music throughout game
/// </summary>
public class MusicManager : MonoBehaviour
{
    // private variables
    static bool initialized = false;            // flag determining whether object has been initialized
    static AudioSource myAudioSource;           // reference to audio source to play sounds from

    /// <summary>
    /// Read-access property returning whether manager has
    /// set its audio source
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes manager by setting an audio source
    /// and pairing music with scene names
    /// </summary>
    public static void Initialize(AudioSource audioSource)
    {
        // set audio source
        initialized = true;
        myAudioSource = audioSource;
    }
}
