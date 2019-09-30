using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            // add self as delegate of OnSceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;

            // initialize audio source and make it persist across scenes
            AudioSource audioSource = GetComponent<AudioSource>();
            MusicManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
            // destroy self if duplicate
            Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene: " + scene.name);
    }
}
